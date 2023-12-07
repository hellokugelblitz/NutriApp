// @ts-nocheck

import { redirect } from "@sveltejs/kit";
import type { Actions, PageServerLoad } from "./$types"

export const load: PageServerLoad = async ({ locals }) => {
	// Get team info
	let team = {
		name: "",
		members: [],
		challengeStartDate: new Date(),
		challengeEndDate: new Date(),
		dataExists: false
	};

	if (!locals.user) return { user: {}, team: team };

	const response = await fetch("http://localhost:5072/api/teams", {
		method: "GET",
		headers: { "sessionKey": locals.user.session_key }
	});

	if (response.status === 200) {
		const responseData = await response.json();
		team = {
			name: responseData.name,
			members: responseData.members,
			challengeStartDate: new Date(responseData.challengeStartDate),
			challengeEndDate: new Date(responseData.challengeEndDate),
			dataExists: true
		}
	}
	else {
		console.log(`${response.status} : ${response.statusText}`)
	}

	return {
		user: locals.user,
		team: team
	};
}

export const actions: Actions = { 
	invite: async ({ request, cookies }) => {
		const data = await request.formData();
		const username = data.get("username");

		console.log(cookies.get("auth"));

		await fetch("http://localhost:5072/api/teams/invite", {
			method: "POST",
			headers: { "sessionKey": cookies.get("auth") },
			body: JSON.stringify({ "username": username })
		}).then(() => {
			throw redirect(303, "/protected/teams");
		});
	},

	create: async ({ request, cookies }) => {
		const data = await request.formData();
		const teamName = data.get("teamName");

		await fetch("http://localhost:5072/api/teams", {
			method: "POST",
			headers: { "sessionKey": cookies.get("auth") },
			body: JSON.stringify({ "teamName": teamName })
		}).then(() => {
			throw redirect(303, "/protected/teams");
		});
	}
};