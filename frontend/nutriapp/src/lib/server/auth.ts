import type { RequestEvent } from "@sveltejs/kit"

export const authenticateUser = (event: RequestEvent) => {
	// get the cookies from the request
	const { cookies } = event

	// get the session key from the cookie
	const sessionKey = cookies.get("auth");

	// If there's no session key, the user is not authenticated
	if (!sessionKey) {
		return null;
	} else {
		// TODO: Validate session key on the backend, here we will pass in the username as well.
		const user = {
			session_key: sessionKey,
            username: "current_user"
		}

		return user;
	}

	return null
}
