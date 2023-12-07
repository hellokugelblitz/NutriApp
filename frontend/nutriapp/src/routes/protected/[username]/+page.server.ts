/** @type {import('./$types').Actions} */
import { error, redirect } from '@sveltejs/kit';
import type { Actions, PageServerLoad } from "./$types"


export const load: PageServerLoad = async ({ locals, fetch, params }) => {
    // Ask back end for the user.
    const fetchUser = async (username: string) => {
        try {
            const res = await fetch(`http://localhost:5072/api/User/${username}`);
            
            // Check that the response is not empty.
            if (!res.ok) {
                throw new Error(`Failed to fetch user for ${username}. Status: ${res.status}`);
            } else if (res.status == 204) {
                throw new Error(`Content is empty for response: ` + res.status);
            }
    
            const data = await res.json();
            console.log(data);
            return data;
        } catch (error: any) {
            console.error(`Error fetching user for ${username},`, error.message);
            return null; // Return null or handle the error as needed
        }
    };

    //Return the user we have grabbed to the front
    return {
        visiting_user: fetchUser(params.username),
		user: locals.user,
    }
};

export const actions = {
	update: async ({cookies, request, locals}) => {
		
        const data = await request.formData();

        const password = data.get('password');
        const name = data.get('name');
        const bio = data.get('bio');

        const body = {
            password: password,
            name: name,
            bio: bio
        }

        try{
            //Handling the event that there is no session key hehe
            let session_key: string = locals.user?.session_key || '';
			const response = await fetch('http://localhost:5072/api/User/update', {
				method: 'POST',
				headers: {
				'Content-Type': 'application/json',
				'sessionKey': session_key
				},
                body: JSON.stringify(body)
			});

			if (response.ok) {
                //Changes have been made

			} else {
                //Had issue making changes
				console.log(response);
			}

		} catch {
            console.log("Something went wrong.")
		}

        throw redirect(302, '/protected/' + locals.user?.username);
	}
} satisfies Actions;