import type { RequestEvent } from "@sveltejs/kit"

export const authenticateUser = async (event: RequestEvent) => {
	// get the cookies from the request
	const { cookies } = event

	// get the session key from the cookie
	const sessionKey = cookies.get("auth");

	// If there's no session key, the user is not authenticated
	if (!sessionKey) {
		return null;
	} else {
		try{
			// Does the user already exist?
			const response = await fetch('http://localhost:5072/api/Auth', {
				method: 'POST',
				headers: {
				'Content-Type': 'application/json',
				},
				body: JSON.stringify(sessionKey),
			});

			if (response.ok) {
				// The user has been authenticated, now we save the username and 
				// session key and return for use in locals.
				const responseData = await response.json();
				const username: string = responseData.userName;

				const user = {
					session_key: sessionKey,
					username: username
				}
				return user;
			} else if (response.status === 401) {
				return null;
			} else {
				console.log(response);
				return null;
			}

		} catch {

		}
	}

	return null
}
