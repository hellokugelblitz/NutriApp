<script>
// @ts-nocheck lol

   import Nav from '$lib/ui/Nav.svelte';
   import { page } from '$app/stores';
   
   export let data;
</script>

<Nav title="Your team" current_data={$page.data}/>

 <!-- GRID -->
 <div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
   <div class="flex-col justify-left col-start-1 col-span-3 row-start-1 row-span-3 p-6 max-h-fit mb-4 border-4 border-gray-225 rounded-full bg-white relative">
      {#if data.team.dataExists}
         <div class="mb-2">
            <span class="text-xl font-bold">{data.team.name}</span>
            <span class="text-lg text-gray-400 ml-4">{data.team.members.length} member(s)</span>
         </div>
         <div class="mb-8">
            {#each data.team.members as member}
               <a href="/protected/{member}">
                  <p class="text-lg text-gray-600 hover:bg-gray-100">{member}</p>
               </a>
            {/each}
         </div>

         <form method="post" action="?/invite">
            <input name="username" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block p-2.5 " placeholder="Username" required>
            <button type="submit" class="text-white bg-dark-green mt-2 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Invite user</button>
         </form>

         <div class="mt-6 mb-2">
            <span class="text-xl font-bold">Current Challenge</span>

            {#if data.team.challengeStartDate.toLocaleDateString("en-us") !== "1/1/1"}
               <span class="text-lg text-gray-400 ml-4">{data.team.challengeStartDate.toLocaleDateString("en-us")} - {data.team.challengeEndDate.toLocaleDateString("en-us")}</span>
            {/if}
         </div>

         {#if data.team.challengeStartDate.toLocaleDateString("en-us") === "1/1/1"}
            <form method="post" action="?/startChallenge">
               <input name="startDate" type="date" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block p-2.5 " placeholder="Start date" required>
               <button type="submit" class="text-white bg-dark-green mt-2 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Start challenge on date</button>
            </form>

         {:else}
            <div>
               <div class="columns-8">
                  <p class="text-lg font-bold">Ranking</p>
                  <p class="text-lg font-bold">Member</p>
                  <p class="text-lg font-bold">Minutes</p>
               </div>

               {#each Object.entries(data.challenge) as entry, i}
                  <div class="columns-8">
                     <p class="text-lg text-gray-600">{i + 1}</p>
                     <p class="text-lg text-gray-600">{entry[0]}</p>
                     <p class="text-lg text-gray-600">{entry[1]}</p>
                  </div>
               {/each}
            </div>
         {/if}

      {:else}
         <div>
            <p class="text-md font-bold mb-8">You are not part of a team!</p>
            <form method="post" action="?/create">
               <input name="teamName" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="Team name" required>
               <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Create team</button>
            </form>
         </div>
      {/if}
   </div>
   {#if data.team.dataExists}
      <a href="/">
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
      </a>
   {/if}
</div>