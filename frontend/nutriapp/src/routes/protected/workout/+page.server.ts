import { redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types"

export const load: PageServerLoad = async ({ locals }) => {
	
	try{
		const workout_response = await fetch('http://localhost:5072/api/Workouts/recommended', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		const all_workouts_response = await fetch('http://localhost:5072/api/History/workouts', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		const goal = await fetch('http://localhost:5072/api/Goal', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		const current_weight = await fetch('http://localhost:5072/api/History/weights', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		if (workout_response.ok && all_workouts_response.ok && current_weight.ok) {
			const workout_response_data = await workout_response.json();
			const all_workout_response_data = await all_workouts_response.json();
			const goal_data = await goal.json();
			const weight_data = await current_weight.json();

			console.log(goal_data)
			return {
				user: locals.user,
				workouts: workout_response_data,
				all_workouts: all_workout_response_data,
				goal: goal_data,
				weight: weight_data
			}
		} else {
			console.log("Here");
		}

	} catch {
		console.log("Something is going wrong with the workouts");
	}

	return {
		user: locals.user,
		workouts: null
	}
}

export const actions = {
	addworkout: async ({cookies, request, locals}) => {
		
        const data = await request.formData();
        const name = String(data.get('name'));
        const minutes = Number(String(data.get('minutes')));
		const intensity = parseFloat(String(data.get('intensity')));

        const body = {
            name: name,
            minutes: minutes,
            intensity: intensity
        }

		//Handling the event that there is no session key hehe
		let session_key: string = locals.user?.session_key || '';
		const response = await fetch('http://localhost:5072/api/Workouts', {
			method: 'POST',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': session_key
			},
			body: JSON.stringify(body)
		});

		if (response.ok) {
			//Changes have been made
			throw redirect(302, '/protected/workout');
		} else {
			//Had issue making changes
			console.log(response);
		}


        throw redirect(302, '/');
	},

	updateweight: async ({cookies, request, locals}) => {
		
        const data = await request.formData();
        const new_weight = Number(String(data.get('weight')));

		console.log(new_weight);

		//Handling the event that there is no session key hehe
		let session_key: string = locals.user?.session_key || '';
		const response = await fetch(`http://localhost:5072/api/Goal/${new_weight}`, {
			method: 'POST',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': session_key
			}
		});

		if (response.ok) {
			//Changes have been made
			throw redirect(302, '/protected/workout');
		} else {
			//Had issue making changes
			console.log(response);
		}

        throw redirect(302, '/');
	},
	updategoal: async ({cookies, request, locals}) => {
		
        const data = await request.formData();
        const new_goal = Number(String(data.get('goal')));

		console.log(new_goal);

		//Handling the event that there is no session key hehe
		let session_key: string = locals.user?.session_key || '';
		const response = await fetch(`http://localhost:5072/api/Goal/${new_goal}`, {
			method: 'PUT',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': session_key
			}
		});

		if (response.ok) {
			//Changes have been made
			console.log(response);
			throw redirect(302, '/protected/workout');
		} else {
			//Had issue making changes
			console.log(response);
		}

        throw redirect(302, '/');
	}
} satisfies Actions;