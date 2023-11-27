<!-- Home page -->
<script>
    import { SvelteToast, toast } from '@zerodevx/svelte-toast'

    //A little tester function
    function sendToast(){
        toast.push('You can do this action yet :(', { classes: ['custom'] })
    }
</script>

<!-- NAVBAR -->
<nav class="mt-0 h-20 bg-white">
    <div class="absolute top-100 left-32 ml-8 transition-transform -translate-x-full md:translate-x-0 md:left-60">
       <h1 class="font-extrabold text-2xl mt-6"> Dashboard </h1>
   </div>
 
   <div class="absolute float-right right-0 mt-5 mr-5">
 
       <button type="button" class="px-3 py-2 text-xs font-medium text-center inline-flex items-center text-black bg-white rounded-lg border-2 border-black border-solid  hover:bg-gray-200 active:ring-4 active:outline-none active:ring-primary-green transition ease-in-out">
          Undo Last Action
 
          <svg class="flex-shrink-0 w-5 h-5 transition duration-75 ml-2 text-white group-hover:text-white" viewBox="0 0 20.298 20.298" xml:space="preserve">
             <path d="M0.952,11.102c0-0.264,0.213-0.474,0.475-0.474h2.421c0.262,0,0.475,0.21,0.475,0.474
                c0,3.211,2.615,5.826,5.827,5.826s5.827-2.615,5.827-5.826c0-3.214-2.614-5.826-5.827-5.826c-0.34,0-0.68,0.028-1.016,0.089
                v1.647c0,0.193-0.116,0.367-0.291,0.439C8.662,7.524,8.46,7.482,8.322,7.347L3.49,4.074c-0.184-0.185-0.184-0.482,0-0.667
                l4.833-3.268c0.136-0.136,0.338-0.176,0.519-0.104c0.175,0.074,0.291,0.246,0.291,0.438V1.96c0.34-0.038,0.68-0.057,1.016-0.057
                c5.071,0,9.198,4.127,9.198,9.198c0,5.07-4.127,9.197-9.198,9.197C5.079,20.299,0.952,16.172,0.952,11.102z"/>
          </svg>
 
       </button>
    </div>
 </nav>

 <!-- GRID -->
<div class=" p-4 md:ml-64 mx-0 max-screen max-w-6xl">
   <!-- TOP ROW -->
   <div class="grid grid-cols-3 gap-5 mb-8 my-4 mx-4">
        <!-- 1 -->
        <div class="flex p-4 flex-col justify-center h-32 bg-primary-green border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">
            <p class="relative top-0 mb-4">Calories Burned:</p>
            <p class="text-4xl mx-4">100</p>
        </div>

        <!-- 2 -->
        <div class="flex p-4 flex-col justify-center h-32 bg-light-green border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">
            <p class="relative top-0 mb-4">Current Weight:</p>
            <p class="text-4xl mx-4">230</p>
        </div>

        <!-- 3 -->
        <div class="flex p-4 flex-col justify-center h-32 bg-nutri-red border-4 border-gray-300 rounded-full font-semibold drop-shadow-sm">                    
            <p class="relative top-0 mb-4">Goal % Complete:</p>
            <p class="text-4xl mx-4">27%</p>
        </div>
    </div>

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
</div>