<script>
   import Nav from '$lib/ui/Nav.svelte';
   import { page } from '$app/stores';
   
   export let data;
</script>

<Nav title="Workouts" current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
   <!-- TOP ROW -->
   <div class="grid grid-cols-3 gap-5 mb-8 my-4 mx-4">
        <!-- 1 -->
        <div class="flex p-4 flex-col justify-center h-32 bg-primary-green border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">
            <p class="relative top-0 mb-4">Calories Burned:</p>
            <p class="text-4xl mx-4">100</p>
        </div>

        <!-- 2 -->
        <div class="flex p-4 flex-col justify-center h-32 bg-light-green border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">
            <p class="relative top-0 mb-4">Current Weight:</p>
            <p class="text-4xl mx-4">230</p>
        </div>

        <!-- 3 -->
        <div class="flex p-4 flex-col justify-center h-32 bg-nutri-red border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">                    
            <p class="relative top-0 mb-4">Goal % Complete:</p>
            <p class="text-4xl mx-4">27%</p>
        </div>
    </div>
</div>

