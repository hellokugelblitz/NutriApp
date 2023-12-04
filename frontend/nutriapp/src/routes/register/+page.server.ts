import { redirect } from '@sveltejs/kit'
import type { Action, Actions, PageServerLoad } from './$types'

export const load:PageServerLoad = async ({ locals }) => {
	return {
		user: locals.user,
	}
}

const register: Action = async ({ request }) => {
    try {
      // We grab the data we need from the form.
      const data = await request.formData();
  
      const username = data.get('username');
      const password = data.get('password');
      const height = 30
      const birthday = "2017-11-01T00:00:00"; // Need to pass a string that is able to be converted to datetime property
      const name = "Jack";
      const currentWeight = 20.00; // Convert to float
      const weightGoal = 40.00;

      console.log("Generating account: " + username);
  
      // Consolidate so that we can pass into the body of the request.
      const credentials = {
        Username: username,
        Password: password,
        Height: height,
        Birthday: birthday,
        Name: name,
        CurrentWeight: currentWeight,
        WeightGoal: weightGoal,
      };
  
    // Does the user already exist?
    const response = await fetch('http://localhost:5072/api/Auth/signup', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(credentials),
    });

    if (response.ok) {
      console.log("Registering: " + username);
    } else if (response.status === 409) {
        console.log("User " + username + " already exists!");
        // Handle conflict scenario (e.g., display an error message to the user)
    } else {
      console.log("\n\n\n\n\n\n\n");
      console.log("Request Data:", JSON.stringify(credentials));
      console.log(response);
      console.log("\n There was an error in registering user: " + username + ", response status: " + response.status);
    }
  } catch (error) {
    // Handle any errors.
    console.error(error);
  }
};

export const actions: Actions = { register }
