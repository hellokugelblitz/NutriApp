<script lang="ts">
    import Nav from '$lib/ui/Nav.svelte';
    import { page } from '$app/stores';

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
    <Nav title='{data.visiting_user.userName} User Profile' current_data={$page.data}/>

    <!-- GRID -->
    <div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
            <h1> Username: <span class="font-bold">{data.visiting_user.userName} </span></h1>
            <h2> Name: <span class="font-bold">{data.visiting_user.name}</span> </h2>
            {#if data.visiting_user.bio == ""}
                <h2> <span class="font-bold"> This user has no BIO </span></h2>
            {:else}
                <h2> User bio: {data.visiting_user.bio} </h2>
            {/if}
    </div>

{/if}