import type { RequestEvent } from "@sveltejs/kit"

export const authenticateUser = (event: RequestEvent) => {
	// get the cookies from the request
	const { cookies } = event

	// get the user token from the cookie
	const userToken = cookies.get("auth")

    //TODO -> This will need to be updated to work with our system. These are placeholder values.
	if (userToken === "regularusertoken") {
        
		const user = {
			id: 1,
            username: "hello"
		}

        console.log("Authenicating user")
		return user
	}

	return null
}
