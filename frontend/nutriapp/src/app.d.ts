// See https://kit.svelte.dev/docs/types#app
// for information about these interfaces

type User = {
	session_key: string
	username: string
}

declare global {		
	namespace App {
		// interface Error {}
		interface Locals {
			user: User | null;
		}
		// interface PageData {}
		// interface Platform {}
	}

}

export {};
