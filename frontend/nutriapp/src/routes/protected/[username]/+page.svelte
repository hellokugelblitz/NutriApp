<script lang="ts">
    import Nav from '$lib/ui/Nav.svelte';
    import { page } from '$app/stores';
    //Grabbing user information.
    export let data;
    const { user } = data;
 </script>
 
 <Nav title={"Placeholder"} current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
    <h1> Usernames goes here </h1>
    <h2> Name goes here </h2>
    <h2> Bio goes here </h2>
</div>