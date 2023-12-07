import { redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types"

export const load: PageServerLoad = async ({ locals }) => {
	
	try{
		const response = await fetch('http://localhost:5072/api/Workouts/recommended', {
			method: 'GET',
			headers: {
			'Content-Type': 'application/json',
			'sessionKey': locals.user?.session_key || ''
			}
		});

		if (response.ok) {
			const data = await response.json();
			console.log(data);
			return {
				user: locals.user,
				workouts: data
			}
		} else {
			console.log(response);
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
			console.log("Added workout: " + name);
			throw redirect(302, '/protected/workout');
		} else {
			//Had issue making changes
			console.log(response);
		}


        throw redirect(302, '/');
	}
} satisfies Actions;