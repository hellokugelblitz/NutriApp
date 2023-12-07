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


export const actions: Actions = {
	consume: async ({ request, locals }) => {
		const data = await request.formData();
		const mealName = data.get('mealName');

		try {
			if (locals.user) {
				const response = await fetch(`http://localhost:5072/api/Meals/consume/${mealName}`, {
					method: 'POST',
					headers: {
						'Content-type': 'application/json',
						"sessionKey": locals.user.session_key,
					}
				});

				if (response.status === 204) {
					redirect(302, '/food');
					alert("Consumed Meal: " + mealName);
				} else {
					console.log("The meal you have provided does not exist: " + mealName);
				}
			}
		} catch (error) {
			console.error("Error consuming meal:", error);
		}
	}
}