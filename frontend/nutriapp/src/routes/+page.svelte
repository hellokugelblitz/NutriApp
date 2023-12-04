<script>
    import { SvelteToast, toast } from '@zerodevx/svelte-toast'

    //A little tester function
    function sendToast(){
        toast.push('You can do this action yet :(', { classes: ['custom'] })
    }

    let list_length = 14;
</script>


<div class="grid grid-cols-3 grid-rows-3 gap-4 mx-4">
    <div class="flex-col justify-left col-span-3 row-span-2 p-6 h-48 mb-4 border-4 border-gray-225 rounded-full bg-white md:col-span-2 relative">
            <p class="text-4xl text-gray-800">
                Hello, you are currently a <span class="font-bold">Guest<span>!
            </p>

            <div class="absolute left-6 bottom-6 align-bottom">
                <button type="button" class="px-3 py-2 text-xs font-medium text-center inline-flex items-center text-black bg-white rounded-lg border-2 border-black border-solid  hover:bg-gray-300 active:ring-4 active:outline-none active:ring-primary-green transition ease-in-out">
                    Sign In
                </button>
                <button type="button" class="px-3 mx-4 py-2 text-xs font-medium text-center inline-flex items-center text-black bg-white rounded-lg border-2 border-black border-solid  hover:bg-gray-300 active:ring-4 active:outline-none active:ring-primary-green transition ease-in-out" on:click={() => sendToast()}>
                    Create an account
                </button>

            </div>
    </div>
    <div class="hidden justify-left col-span-1 p-6 h-48 border-4 border-gray-225 rounded-full bg-white md:flex">
        <p class="text-xl text-gray-800">
            Your History
        </p>
    </div>
</div>