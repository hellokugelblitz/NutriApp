<script lang="ts">
    import Nav from '$lib/ui/Nav.svelte';
    import { page } from '$app/stores';
    import profilepic from '$lib/assets/profilepic.jpg';

    //Grabbing information.
    export let data;
    const { visiting_user } = data;
    const { user } = data;
 </script>
 
 <!-- Here we are checking to see if the user we are currently logged in as is the user we are trying to view! -->
 {#if $page.data.user.username == data.visiting_user.userName}
    <Nav title='Your profile' current_data={$page.data}/>

    <!-- GRID -->
    <div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">

            <div class="p-4 md:p-5 w-96">
                <!-- "action" here is the api endpoint for auth -->
                <form class="space-y-4" method="POST">
                    <div>
                        <label for="username" class="block mb-2 text-sm font-medium text-gray-900 ">Your username:</label>
                        <input type="username" name="username" id="username" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " value="{$page.data.user.username}" required>
                    </div>
                    <div>
                        <label for="password" class="block mb-2 text-sm font-medium text-gray-900 ">Your password:</label>
                        <input type="password" name="password" id="password" placeholder="••••••••" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " required>
                    </div>
                    <div>
                        <label for="name" class="block mb-2 text-sm font-medium text-gray-900 ">Your name:</label>
                        <input type="text" name="name" id="name" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " value="{data.visiting_user.name}" required>
                    </div>
                    <div>
                        <label for="bio" class="block mb-2 text-sm font-medium text-gray-900">Your Bio:</label>
                        <textarea name="bio" id="bio" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 h-24" value="{data.visiting_user.bio}"></textarea>
                    </div>
                    <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Update Profile</button>
                </form>
            </div>
    </div>

{:else}
    <Nav title='User Profile: {data.visiting_user.userName} ' current_data={$page.data}/>

    <!-- GRID -->
    <div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl relative">
        <div class="absolute content-center justify-center items-center border-4 border-gray-225 rounded-full bg-white p-8 w-96 h-96">
            <div class="relative flex flex-col justify-center items-center">
                <img class="w-28 h-28 p-1 rounded-max ring-2 ring-primary-green" alt="The project logo" src={profilepic} />
                <h1><span class="font-bold text-lg">{data.visiting_user.userName} </span></h1>
                <h2><span class="italic text-gray-300">{data.visiting_user.name}</span> </h2>
            </div>
            {#if data.visiting_user.bio == ""}
                <div class="relative pt-8 h-32">
                    <h2> <span class="absolute font-bold left-4 top-2"> User Bio: </span></h2>
                    <div class="absolute bg-gray-100 rounded-full p-4 top-8 w-full h-full">
                        <h2><span class="font-bold italic text-gray-300">This user has no Bio</span> </h2>
                    </div>
                </div>
            {:else}
                <div class="relative pt-8 h-32">
                    <h2> <span class="absolute font-bold left-4 top-2"> User Bio: </span></h2>
                    <div class="absolute bg-gray-100 rounded-full p-4 top-8 w-full h-full">
                        <h2><span class="text-black">{data.visiting_user.bio}</span> </h2>
                    </div>
                </div>
            {/if}
        </div>
    </div>

{/if}