import { redirect } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

//Removes auth cookie, redirects user.
export const POST: RequestHandler = async ({ cookies }) => {
	cookies.delete("auth")
	throw redirect(303, "/")
}