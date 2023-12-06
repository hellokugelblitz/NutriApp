/** @type {import('./$types').Actions} */
import { error } from '@sveltejs/kit';
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
	updateUsername: async (event) => {
		// TODO 
	},
    updatePassword: async (event) => {
		// TODO 
	},
    updateName: async (event) => {
		// TODO
	},
    updateBio: async (event) => {
		// TODO
	}
} satisfies Actions;