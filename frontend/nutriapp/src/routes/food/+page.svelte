<script>
    import { page } from '$app/stores';
    import Nav from '$lib/ui/Nav.svelte';
    import { SvelteToast, toast } from '@zerodevx/svelte-toast'

    function sendToast(){
        toast.push('You can do this action yet :(', { classes: ['custom'] })
    }
</script>

<Nav title="Food" current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
    <h1> Food will go here </h1>
</div>