// @ts-nocheck

import { redirect } from "@sveltejs/kit";

export const load: PageServerLoad = async ({ locals, params }) => {
    const response = await fetch("http://localhost:5072/api/teams/accept", {
		method: "PUT",
		headers: { "sessionKey": locals.user.session_key },
        body: JSON.stringify({ "inviteCode": params.code })
	});

    throw redirect(303, "/protected/teams");
}