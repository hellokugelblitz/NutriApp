import { redirect } from '@sveltejs/kit'
import type { Action, Actions, PageServerLoad } from './$types'

export const load:PageServerLoad = async () => {
    // page load
}

//The register action
const register : Action = async({ request }) => {
    //We grab the data we need from the form.
    const data = await request.formData();

    const username = data.get('username');
    const password = data.get('password');

    //Consolidate so that we can pass into the body of the request.
    const credentials = {
        userName: username,
        password: password,
    };
    
    //Does user already exist?
    try {
        const response = await fetch('http://localhost:5072/api/Auth/register', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(credentials),
        });
  
        if (response.ok) {
            console.log("Registering: " + username);
        }
    } catch {
        //Handle any errors.
    }

}

export const actions: Actions = { register }
