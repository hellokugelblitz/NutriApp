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

      // Sanitize the user input for our fetch request.
      const username = data.get('username');
      const password = data.get('password');
      const birthday = `${data.get('birthday')}T00:00:00`; // translate given input 2023-12-13 to "2023-12-13T00:00:00"

      const heightString = String(data.get('height')); // We must parse the height of the user into inches.
      let height = 0;
      if(heightString != null){
        const [feet, inches] = heightString.split("'").map(part => parseInt(part, 10));
        height = feet * 12 + inches; // Calculate the height in inches
      }

      const name = data.get('name');
      const currentWeight = parseFloat(String(data.get('weight'))); // Convert to float
      const weightGoal = parseFloat(String(data.get('goal')));

  
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


      console.log("Generating account: " + credentials);
  
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

  throw redirect(302, '/login');
};

export const actions: Actions = { register }
