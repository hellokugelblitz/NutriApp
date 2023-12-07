import type { Action, Actions, PageServerLoad } from './$types'
import { redirect } from '@sveltejs/kit'

export const load:PageServerLoad = async () => {
    // page load
}

// const exportUser : Action =async ({locals}) => {
//     console.log('export');
//     const sessionKey = locals.user?.session_key || ""
//     const response = await fetch('http://localhost:5072/api/Save/export/user', {
//     method: 'GET',
//     headers: {
//         sessionKey: sessionKey
//     }
//     });
//     console.log(response)
    
//     const blob = await response.blob();
//     const url = URL.createObjectURL(blob);

//     // Create a link element
//     const link = document.createElement('a');
//     link.href = url;
//     link.download = 'exported_user_data.csv'; // You can set the desired filename

//     // Append the link to the body
//     document.body.appendChild(link);

//     // Trigger a click on the link to start the download
//     link.click();

//     // Remove the link from the DOM
//     document.body.removeChild(link);

//     // Revoke the URL to free up resources
//     URL.revokeObjectURL(url);
// }
//The register action
const login : Action = async({ request, cookies }) => {
    //We grab the data we need from the form.
    const data = await request.formData();

    const username = data.get('username');
    const password = data.get('password');

    //Consolidate so that we can pass into the body of the request.
    const credentials = {
        userName: username,
        password: password,
    };

    //Attempt to login
    try {
        const response = await fetch('http://localhost:5072/api/Auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(credentials),
        });
  
        if (response.ok) {
            console.log("Logging in: " + username);
            const result = await response.json();

            //Here we establish our authentication cookie yay!
            cookies.set("auth", result.session, {
			    path: "/",
			    httpOnly: true,
			    sameSite: "strict",
			    secure: process.env.NODE_ENV === "production",
			    maxAge: 60 * 60 * 24 * 7, // 1 week
		    });
        } else {
            console.log("Credentials not recognized!")
        }
    } catch {
        //Handle any errors.
    }

    throw redirect(302, '/');
}



export const actions: Actions = { login, exportUser: async ({locals}) => {
    console.log('export');
    const sessionKey = locals.user?.session_key || ""
    const response = await fetch('http://localhost:5072/api/Save/export/user', {
    method: 'GET',
    headers: {
        sessionKey: sessionKey
    }
    });
    console.log(response)
    
    const blob = await response.blob();
    const url = URL.createObjectURL(blob);

    // Create a link element
    const link = document.createElement('a');
    link.href = url;
    link.download = 'exported_user_data.csv'; // You can set the desired filename

    // Append the link to the body
    document.body.appendChild(link);

    // Trigger a click on the link to start the download
    link.click();

    // Remove the link from the DOM
    document.body.removeChild(link);

    // Revoke the URL to free up resources
    URL.revokeObjectURL(url);
} }
