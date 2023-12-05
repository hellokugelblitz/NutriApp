<script lang="ts">
    import { SvelteToast, toast } from '@zerodevx/svelte-toast'
    import { page } from '$app/stores';
    import Nav from '$lib/ui/Nav.svelte';

    //A little tester function
    function sendToast(){
        toast.push('You can do this action yet :(', { classes: ['custom'] })
    }

    //Grab the data for the user.
    export let data;

    //For the test history
    let list_length = 14;
</script>

 <Nav title="Dashboard" current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl max-h-full">

    <div class="grid grid-cols-3 grid-rows-2 gap-4">

        <div class="flex-col justify-left col-start-1 col-span-3 row-start-1 row-span-3 p-6 max-h-fit mb-4 border-4 border-gray-225 rounded-full bg-white relative">
            {#if !$page.data.user}
                <p class="text-4xl text-gray-800 font-extrabold">
                    Hello, you are currently a <span class="font-bold text-primary-green">Guest<span>!
                </p>
            {/if}

            {#if $page.data.user}
                <p class="text-4xl text-gray-800 font-extrabold">
                    Hello, <span class="font-bold text-primary-green">{data.user?.username}!</span>
                </p>
            {/if}

            {#if !$page.data.user}
                <p class="text-2xl text-gray-500 my-7">Feel free to browse our list of meals and recipes, but log in to gain access to the full features of Nutriapp!</p>
            {:else}
                <p class="text-2xl text-gray-500 my-7">Welcome to Nutriapp! Delve into our diverse selection of meals and recipes at your leisure. Enjoy the complete Nutriapp experience with enhanced features tailored just for you. Happy browsing! </p>
            {/if}
                <div class="hidden mlg:block twoxl:block sm:hidden md:hidden lg:hidden">
                    <!-- Do this with Nutriapp section -->
                    <div class="flex flex-row space-x-10 items-center justify-center">
                        <div class="flex flex-col items-center justify-center">
                            <p class="text-sm font-light py-2 text-gray-800"> Track your meals </p>
                            <div class="p-5 bg-gray-50 rounded-lg">
                                <svg class="flex-shrink-0 w-20 h-20 text-primary-green transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" fill="#6A994E" viewBox="0 -3.84 122.88 122.88">
                                    <path d="M29.03,100.46l20.79-25.21l9.51,12.13L41,110.69C33.98,119.61,20.99,110.21,29.03,100.46L29.03,100.46z M53.31,43.05 c1.98-6.46,1.07-11.98-6.37-20.18L28.76,1c-2.58-3.03-8.66,1.42-6.12,5.09L37.18,24c2.75,3.34-2.36,7.76-5.2,4.32L16.94,9.8 c-2.8-3.21-8.59,1.03-5.66,4.7c4.24,5.1,10.8,13.43,15.04,18.53c2.94,2.99-1.53,7.42-4.43,3.69L6.96,18.32 c-2.19-2.38-5.77-0.9-6.72,1.88c-1.02,2.97,1.49,5.14,3.2,7.34L20.1,49.06c5.17,5.99,10.95,9.54,17.67,7.53 c1.03-0.31,2.29-0.94,3.64-1.77l44.76,57.78c2.41,3.11,7.06,3.44,10.08,0.93l0.69-0.57c3.4-2.83,3.95-8,1.04-11.34L50.58,47.16 C51.96,45.62,52.97,44.16,53.31,43.05L53.31,43.05z M65.98,55.65l7.37-8.94C63.87,23.21,99-8.11,116.03,6.29 C136.72,23.8,105.97,66,84.36,55.57l-8.73,11.09L65.98,55.65L65.98,55.65z"/>
                                </svg>
                            </div>
                        </div>

                        <div class="flex flex-col items-center justify-center">
                            <p class="text-sm font-light py-2 text-gray-800"> Push notifications </p>
                            <div class="p-5 bg-gray-50 rounded-lg">
                                <svg class="flex-shrink-0 w-20 h-20 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="#6A994E" viewBox="0 0 20 20">
                                    <path d="m17.418 3.623-.018-.008a6.713 6.713 0 0 0-2.4-.569V2h1a1 1 0 1 0 0-2h-2a1 1 0 0 0-1 1v2H9.89A6.977 6.977 0 0 1 12 8v5h-2V8A5 5 0 1 0 0 8v6a1 1 0 0 0 1 1h8v4a1 1 0 0 0 1 1h2a1 1 0 0 0 1-1v-4h6a1 1 0 0 0 1-1V8a5 5 0 0 0-2.582-4.377ZM6 12H4a1 1 0 0 1 0-2h2a1 1 0 0 1 0 2Z"/>
                                </svg>
                            </div>
                        </div>

                        <div class="flex flex-col items-center justify-center">
                            <p class="text-sm font-light py-2 text-gray-800"> Team Challenges </p>
                            <div class="p-5 bg-gray-50 rounded-lg">
                                <svg class="flex-shrink-0 w-20 h-20 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="#6A994E" viewBox="0 0 20 18">
                                    <path d="M14 2a3.963 3.963 0 0 0-1.4.267 6.439 6.439 0 0 1-1.331 6.638A4 4 0 1 0 14 2Zm1 9h-1.264A6.957 6.957 0 0 1 15 15v2a2.97 2.97 0 0 1-.184 1H19a1 1 0 0 0 1-1v-1a5.006 5.006 0 0 0-5-5ZM6.5 9a4.5 4.5 0 1 0 0-9 4.5 4.5 0 0 0 0 9ZM8 10H5a5.006 5.006 0 0 0-5 5v2a1 1 0 0 0 1 1h11a1 1 0 0 0 1-1v-2a5.006 5.006 0 0 0-5-5Z"/>
                                </svg>
                            </div>
                        </div>

                        <div class="flex flex-col items-center justify-center">
                            <p class="text-sm font-light py-2 text-gray-800"> Track Workouts </p>
                            <div class="p-5 bg-gray-50 rounded-lg">
                                <svg class="flex-shrink-0 w-20 h-20 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" fill="#6A994E" viewBox="0 -1.26 122.88 122.88">
                                    <path class="st0" d="M8.94,102.48h85.62c3.71-3.85,8.02-14.75,12.45-25.96c1.41-3.57,2.83-7.17,4.3-10.66 c-6.43,0.02-15.18-0.61-16.52-2.15c-1.04-1.19-0.66-2.93,0.91-5.12l18.93-18.48c1.77-1.6,3.31-2.21,4.62-1.81 c1.3,0.39,2.38,1.76,3.23,4.13c0.48,7.13,0.43,14.27-0.63,21.4c-0.58,0.34-1.18,0.63-1.79,0.89c-2.01,4.48-4.01,9.56-6,14.58 c-3.41,8.63-6.75,17.09-10.52,23.18h10.39c4.92,0,8.94,4.02,8.94,8.94v0c0,4.92-4.02,8.94-8.94,8.94h-105 c-4.92,0-8.94-4.02-8.94-8.94v0C0,106.51,4.02,102.48,8.94,102.48L8.94,102.48z M36.43,36.39l-0.27-0.45l-7.06,1.01L26.54,52.6 c-0.48,2.93-3.23,4.91-6.16,4.44c-2.93-0.48-4.91-3.23-4.44-6.16l3.2-19.55c0.39-2.38,2.28-4.13,4.54-4.45l0,0l13.67-1.96 l10.11-2.21c5.76,0.19,10.06,2.61,12,7.7c0.28,0.35,0.52,0.75,0.71,1.18l5.42,12.39l17.68,1.1c2.96,0.17,5.22,2.72,5.04,5.68 c-0.17,2.96-2.72,5.22-5.68,5.05l-20.97-1.3c-2.16-0.13-3.95-1.52-4.69-3.41l-1.1-2.51l-2.25,9.79l14.84,8.74 c1.29,0.75,2.14,1.96,2.48,3.3l0.02,0l5.39,21.11c0.73,2.88-1.01,5.81-3.89,6.54c-2.88,0.73-5.81-1.01-6.54-3.89L61.1,75.19 l-1.24-0.73l-15.42-5.87l-0.83,0.46l-4.71,12.49c-0.34,0.9-0.89,1.65-1.58,2.23l0,0L19.65,98.6c-2.28,1.9-5.66,1.6-7.57-0.67 c-1.9-2.28-1.6-5.66,0.67-7.57L29.03,76.7C30.22,62.9,32.82,49.5,36.43,36.39L36.43,36.39z M57.91,0c5.64,0,10.21,4.57,10.21,10.21 c0,5.64-4.57,10.21-10.21,10.21c-5.64,0-10.21-4.57-10.21-10.21C47.7,4.57,52.27,0,57.91,0L57.91,0z"/>
                                </svg>
                            </div>
                        </div>
                    </div>
                </div>
        </div>

        <!-- History view panel -->
        {#if $page.data.user}
            <div class="hidden justify-left col-span-1 row-span-2 col-start-1 row-start-1 p-6 mb-4 h-96 w-full border-4 border-primary-green rounded-full bg-white md:col-span-2 md:flex flex-col">
                <p class="text-xl text-gray-800 pb-8">
                    Your History
                </p>

                <div class="bg-gray-200 w-full h-3/4 overflow-y-scroll rounded-full">
                    <!-- This is placeholder -->
                    <ul>
                        {#each {length: list_length} as _, i} 
                            <!-- Bottom of list -->
                            {#if i > list_length-2}
                                <li class="list-none list-inside">
                                    <div class="border-gray-500 w-full h-12 px-8 py-3">
                                        <p>Added Meal: <span class="font-bold">GURL DINNER</span></p>
                                    </div>
                                </li>

                            <!-- Other list items -->
                            {:else}
                                <li class="list-none list-inside">
                                    <div class="border-gray-500 border-b-2 w-full h-12 px-8 py-3">
                                        <p>Added Meal: <span class="font-bold">Girl Dinner</span></p>
                                    </div>
                                </li>
                            {/if}
                        {/each}
                    </ul>
                </div>
            </div>
            {/if}
    </div>
</div>