import { redirect } from "@sveltejs/kit";
import type { Action, Actions, PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ locals }) => {
	try {
		const [ingredientsResponse, recipesResponse, mealsResponse] = await Promise.all([
			fetch('http://localhost:5072/api/Ingredients'),
			fetch('http://localhost:5072/api/Recipes'),
			fetch('http://localhost:5072/api/Meals')
		]);

		const ingredients = await ingredientsResponse.json();
		const recipes = await recipesResponse.json();
		const meals = await mealsResponse.json();

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

const consumeMeal: Action = async (mealName) => {
	try {
		const response = await fetch(`http://localhost:5072/api/Meals/consume/${mealName}`, {
			method: 'POST'
		});

		
	} catch (error) {
		console.error("Error consuming meal:", error);
	}
}