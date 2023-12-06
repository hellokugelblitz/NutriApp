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
      {data.user.session_key}

      <p class="text-xl font-bold">{data.team["name"]}</p>

      <p class="text-lg mt-4 mb-2">Members</p>
      {#each data.team.members as member}
         <p class="text-gray-600">{member}</p>
      {/each}

      <p class="text-lg mt-6">Current Challenge</p>
      <p class="text-md mb-4 text-gray-400">{data.team.challengeStartDate.toDateString()} - {data.team.challengeEndDate.toDateString()}</p>
      <p>(TODO)</p>
   </div>
</div>