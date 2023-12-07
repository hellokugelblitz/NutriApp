<script>
// @ts-nocheck lol

   import Nav from '$lib/ui/Nav.svelte';
   import { page } from '$app/stores';
    import { redirect } from '@sveltejs/kit';
    import { goto } from '$app/navigation';
   
   let username = '';
   let teamName = '';
   export let data;
</script>

<Nav title="Your team" current_data={$page.data}/>

 <!-- GRID -->
 <div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
   <div class="flex-col justify-left col-start-1 col-span-3 row-start-1 row-span-3 p-6 max-h-fit mb-4 border-4 border-gray-225 rounded-full bg-white relative">
      {#if data.team.dataExists}
         <div class="mb-2">
            <span class="text-xl font-bold">{data.team.name}</span>
            <span class="text-md text-gray-400">({data.team.members.length} members)</span>
         </div>
         <div class="mb-8">
            {#each data.team.members as member}
               <p class="text-lg text-gray-600">{member}</p>
            {/each}
         </div>

         <form method="post" action="?/invite">
            <input bind:value={username} name="username" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block p-2.5 " placeholder="Username" required>
            <button type="submit" class="text-white bg-dark-green mt-2 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Invite user</button>
         </form>

         <div class="mt-6 mb-2">
            <span class="text-xl font-bold">Current Challenge</span>
            <span class="text-md text-gray-400">({data.team.challengeStartDate.toDateString()} - {data.team.challengeEndDate.toDateString()})</span>
         </div>
         TODO

      {:else}
         <div>
            <p class="text-md font-bold mb-8">You are not part of a team!</p>
            <form method="post" action="?/create">
               <input bind:value={teamName} name="teamName" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="Team Name" required>
               <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Create team</button>
            </form>
         </div>
      {/if}
   </div>
   {#if data.team.dataExists}
      <button
         on:click={() => {
            const response = fetch("http://localhost:5072/api/teams/leave", {
               method: "PUT",
               headers: { "sessionKey": data.user.session_key }
            });
         }}
         class="text-white bg-dark-green ml-1 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all"
      >
         Leave team
      </button>
   {/if}

   <p class="text-gray-400">{data.user.session_key}</p>
</div>