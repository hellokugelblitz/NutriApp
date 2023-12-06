// @ts-nocheck

export const load: PageServerLoad = async ({ params }) => {
    return {
        inviteCode: params.code
    }
}