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
            <p> This is you </p>
        
            <h1> Username: <span class="font-bold">{data.visiting_user.userName} </span></h1>
            <h2> Name: <span class="font-bold">{data.visiting_user.name}</span> </h2>
            {#if data.visiting_user.bio == ""}
                <h2> <span class="font-bold"> This user has no BIO </span></h2>
            {:else}
                <h2> User bio: {data.visiting_user.bio} </h2>
            {/if}
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