<script>
   import Nav from '$lib/ui/Nav.svelte';
   import { page } from '$app/stores';
   
   export let data;
   const { workouts } = data;
</script>

<Nav title="Workouts" current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-8xl">
   <!-- TOP ROW -->
   <div class="grid grid-cols-3 gap-5 mb-8 my-4 mx-4">
        <!-- 1 -->
        <div class="relative flex p-4 flex-col col-span-2 justify-center h-96 bg-white border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm w-full">
            <p class="absolute top-4 mb-8">Previous Workouts:</p>
            <div class="bg-gray-200 w-full h-3/4 overflow-y-scroll rounded-full">
                <ul>
                    {#each workouts as workout, i}
                      <li class="list-none list-inside" class:even={i % 2 === 0}>
                        <div class="border-gray-500 border-b-2 w-full h-24 px-8 py-3">
                          <p>Workout Name: <span class="font-bold">{workout.name}</span></p>
                          <p>Time Spent: {workout.minutes} minutes</p>
                          <p>Intensity: {workout.intensity}</p>
                        </div>
                      </li>
                    {/each}
                  </ul>
            </div>
        </div>

        <!-- 3 -->
        <div class="flex p-4 flex-col col-start-3 justify-center h-96 w-3/4 bg-white border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">                    
            <p class="absolute top-4 mb-8">Create new workout:</p>
            <form class="space-y-4 w-3/4 absolute top-12" method="POST" action="?/addworkout">
                <div>
                    <label for="name" class="block mb-2 text-sm font-medium text-gray-900 ">Workout Name:</label>
                    <input type="text" name="name" id="name" placeholder="Crunches" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 ">
                </div>
                <div>
                    <label for="minutes" class="block mb-2 text-sm font-medium text-gray-900 ">Time Spent:</label>
                    <input type="number" name="minutes" id="minutes" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " value="">
                </div>
                <div>
                    <label for="intensity" class="block mb-2 text-sm font-medium text-gray-900 ">Intensity:</label>
                    <select id="intensity" name="intensity" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
                        <option selected value="5">Low</option>
                        <option value="10">Medium</option>
                        <option value="15">High</option>
                      </select>
                </div>
                <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Create Workout</button>
            </form>
        </div>
    </div>
</div>

