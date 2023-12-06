<script>
// @ts-nocheck

    import Nav from '$lib/ui/Nav.svelte';
    import { page } from '$app/stores';

    export let data;
 </script>
 
 <Nav title="Notifications" current_data={$page.data}/>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
    <div class="flex-col justify-left col-start-1 col-span-3 row-start-1 row-span-3 p-6 max-h-fit mb-4 border-4 border-gray-225 rounded-full bg-white relative">
        {#if data.user.notifications.length == 0}
            <p class="text-md font-bold">You don't have any notifications.</p>
        
        {:else}
            {#each data.user.notifications as notification}
                <span class="text-gray-400">{new Date(notification.timestamp).toLocaleString("en-us")}</span>
                <span class="ml-3">{notification.contents}</span>

                <span>
                    <a href={notification.url}>
                        <button
                            type="button"
                            class="mx-8 px-3 py-1 float-center text-xs font-medium items-center text-black bg-white rounded-lg border-2 border-black border-solid  hover:bg-gray-200 active:ring-4 active:outline-none active:ring-primary-green transition ease-in-out">
                            {notification.buttonText}
                        </button>
                    </a>
                </span>
            {/each}
        {/if}
    </div>
</div>