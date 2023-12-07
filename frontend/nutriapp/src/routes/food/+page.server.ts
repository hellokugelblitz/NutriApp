import { redirect } from "@sveltejs/kit";
import type { Action, Actions, PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
	try {
		const [ingredientsResponse, recipesResponse, mealsResponse] = await Promise.all([
			fetch('http://localhost:5072/api/Ingredients'),
			fetch('http://localhost:5072/api/Recipes'),
			fetch('http://localhost:5072/api/Meals')
		]);

		const ingredients = await ingredientsResponse.json() || [];
		const recipes = await recipesResponse.json() || [];
		const meals = await mealsResponse.json() || [];

		return {
			user: locals.user,
			ingredients: ingredients.slice(0, 10),
			recipes: recipes.slice(0, 10),
			meals: meals.slice(0, 10),
		};
	} catch (error) {
		console.error("Error fetching data:", error);
		return {
			user: locals.user,
			ingredients: [],
			recipes: [],
			meals: []
		};
	}
}




export const actions: Actions = {
	consume: async ({ request, locals }) => {
		const data = await request.formData();
		const mealName = data.get('mealName');

		try {
			const response = await fetch(`http://localhost:5072/api/Meals/consume/${mealName}`, {
				method: 'POST',
				headers: {
					'Content-type': 'application/json',
					"sessionKey": locals.user?.session_key || '',
				}
			});

			if (response.status === 204) {
				console.log("Meal consumed successfully: " + mealName);
				throw redirect(302, "/food");
			} else {
				console.log("The meal you have provided does not exist: " + mealName);
			}
		} catch (error) {
			if (error instanceof Error) {
				console.error("Error consuming meal:", error);
			}
		}
	},

	recipe: async ({ request, locals }) => {
		const data = await request.formData();
		const recipeName = data.get('recipeName') as string;
		const recipeInstructions = [];
		const recipeIngredients = {} as { [key: string]: number };

		for (let i = 0; data.has(`instruction${i}`); i++) {
			const instruction = data.get(`instruction${i}`) as string;
			if (instruction) {
				recipeInstructions.push(instruction);
			}
		}

		for (let i = 0; data.has(`ingredientName${i}`); i++) {
			const ingredientName = data.get(`ingredientName${i}`) as string;
			const ingredientQuantity = parseInt(data.get(`ingredientQuantity${i}`) as string);

			if (ingredientName && !isNaN(ingredientQuantity)) {
				recipeIngredients[ingredientName] = ingredientQuantity;
			}
		}

		const recipeData = {
			name: recipeName,
			instructions: recipeInstructions,
			ingredients: recipeIngredients
		};

		console.log(recipeData);

		try {
			const response = await fetch(`http://localhost:5072/api/Recipes/`, {
				method: 'POST',
				headers: {
					'Content-type': 'application/json',
					"sessionKey": locals.user?.session_key || '',
				},
				body: JSON.stringify(recipeData)
			});

			if (response.status === 201) {
				console.log("Recipe added successfully: " + recipeName);
				throw redirect(302, "/food");
			} else {
				console.log("The ingredients you added doesn't exist: " + recipeIngredients);
			}
		} catch (error) {
			if (error instanceof Error) {
				console.error("Error adding recipe:", error);
			}
		}
	},

	meal: async ({ request, locals }) => {
		const data = await request.formData();
		const mealName = data.get('mealName');
		const mealRecipes = {} as { [key: string]: number };

		for (let i = 0; data.has(`recipeName${i}`); i++) {
			const recipeName = data.get(`recipeName${i}`) as string;
			const recipeQuantity = parseInt(data.get(`recipeQuantity${i}`) as string);

			if (recipeName && !isNaN(recipeQuantity)) {
				mealRecipes[recipeName] = recipeQuantity;
			}
		}

		const mealData = {
			name: mealName,
			recipes: mealRecipes
		};

		console.log(mealData);
		console.log(JSON.stringify(mealData));

		try {
			const response = await fetch(`http://localhost:5072/api/Meals/`, {
				method: 'POST',
				headers: {
					'Content-type': 'application/json',
					"sessionKey": locals.user?.session_key || '',
				},
				body: JSON.stringify(mealData)
			});

			if (response.status === 201) {
				console.log("Meal added successfully: " + mealName);
				throw redirect(302, "/food");
			} else {
				console.log("The recipes you added doesn't exist: " + mealRecipes);
			}
		} catch (error) {
			if (error instanceof Error) {
				console.error("Error adding meal:", error);
			}
		}
	}
}