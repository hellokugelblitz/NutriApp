// @ts-nocheck

import { redirect } from "@sveltejs/kit";

export const load: PageServerLoad = async ({ locals }) => {
    const response = await fetch("http://localhost:5072/api/teams/accept", {
		method: "PUT",
		headers: { "sessionKey": locals.user.session_key },
        body: { "inviteCode": locals.code }
	}).then(() => {
        throw redirect(303, "/protected/teams");
    }).catch((error) =>{
        console.log(error);
    });
}