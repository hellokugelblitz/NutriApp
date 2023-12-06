<script>
    import Nav from '$lib/ui/Nav.svelte';
    import { page } from '$app/stores';

    export let data;
 </script>
 
 <Nav title="Notifications" current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
    <h1> Notifications will go here </h1>
</div>