import { redirect } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

//Removes auth cookie, redirects user.
export const POST: RequestHandler = async ({ cookies, locals, request }) => {
	    //Attempt to login
		try {
			let session_key = locals.user?.session_key || '';
			console.log("Session key: " + session_key);
			const response = await fetch('http://localhost:5072/api/Auth/logout', {
			  method: 'POST',
			  headers: {
				'Content-Type': 'application/json',
				'sessionKey': session_key
			  }
			});
	  
			if (response.status == 204) {
				cookies.delete("auth");
				throw redirect(303, "/");
			} else {
				console.log(response);
				console.log("Strange server response during logout: " + response.status);
			}
		} catch {
			console.log("Something went wrong during logout");
		}
	throw redirect(303, "/");
}