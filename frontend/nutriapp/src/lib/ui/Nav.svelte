<script lang="ts">
   import profilepic from '$lib/assets/profilepic.jpg';

    export let title: string;
    export let current_data: any;

   function undo(element: HTMLElement){
      //
   }
</script>

<nav class="mt-0 h-20 bg-white">
    <div class="absolute top-100 left-32 ml-8 transition-transform -translate-x-full md:translate-x-0 md:left-60">
       <h1 class="font-extrabold text-2xl mt-6"> {title} </h1>
   </div>
 
   <div class="absolute float-right right-0 mt-5 mr-5">
    
    <!-- Checking that the user is authenticated -->
    {#if current_data.user}
    <div class="flex flex-row gap-8">
       <a href="/protected/{current_data.user.username}">
         <img class="w-10 h-10 p-1 rounded-max ring-2 ring-primary-green hover:ring-dark-green transition" alt="The project logo" src={profilepic} />
       </a>
    </div>
    {/if}

    </div>
 </nav>