import { redirect } from "@sveltejs/kit"
import type { Actions, PageServerLoad } from "./$types"

export const load: PageServerLoad = async ({ locals }) => {

	//If the user is authenticated
	if(locals.user){
		const workout_history = await fetch('http://localhost:5072/api/History/workouts', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		const weight_history = await fetch('http://localhost:5072/api/History/weights', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		const calorie_history = await fetch('http://localhost:5072/api/History/calories', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		const meal_history = await fetch('http://localhost:5072/api/History/meals', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		if (
			workout_history.ok &&
			weight_history.ok &&
			calorie_history.ok &&
			meal_history.ok
		) {
			// Parse JSON data from each response
        // Parse JSON data from each response
        const workout_data: any[] = await workout_history.json();
        const weight_data: any[] = await weight_history.json();
        const calorie_data: any[] = await calorie_history.json();
        const meal_data: any[] = await meal_history.json();

        // Combine all history types into a single array
        const allHistory: any[] = [...workout_data, ...weight_data, ...calorie_data, ...meal_data];
        // Sort the combined array by timestamp in descending order
		allHistory.sort((a, b) => new Date(b.timeStamp).getTime() - new Date(a.timeStamp).getTime());	
		console.log(workout_data);
		console.log(weight_data);
		console.log(calorie_data);
		console.log(meal_data);
		console.log(allHistory);
	
			return {
				user: locals.user,
				workouts: workout_data,
				weights: weight_data,
				calories: calorie_data,
				meals: meal_data,
				all_history: allHistory
			};
		} else {
			console.log("Error fetching history data");
		}
	}

	return {
		user: locals.user,
		workouts: null,
		weights: null,
		calories: null,
		meals: null,
		all_history: null
	}
}



// export const actions: Actions = {
// 	login: async ({ cookies }) => {
//         console.log("logging in...")
// 		cookies.set("auth", "regularusertoken", {
// 			path: "/",
// 			httpOnly: true,
// 			sameSite: "strict",
// 			secure: process.env.NODE_ENV === "production",
// 			maxAge: 60 * 60 * 24 * 7, // 1 week
// 		})

// 		throw redirect(303, "/")
// 	},
// }

// Example cookie creation ^^^^