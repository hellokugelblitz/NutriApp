<script>
    import { closeModal } from 'svelte-modals'
  
    // provided by <Modals />
    export let isOpen = false;
    export let title = "";
  </script>
  
{#if isOpen}
   <div role="dialog" class="modal">
      <div class="contents">
          <!-- Main modal -->
        <div id="authentication-modal" tabindex="-1" aria-hidden="true" class="modal hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full md:ml-28">
          <div class="relative p-4 w-full max-w-md max-h-full">
              <!-- Modal content -->
              <div class="relative bg-white rounded-lg shadow border-dark-green border-4">
                  <!-- Modal header -->
                  <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t ">
                      <h3 class="font-extrabold text-2xl text-gray-90">
                          {title}
                      </h3>
                      <button on:click={closeModal} type="button" class="end-2.5 text-gray-400 bg-transparent hover:gray-700 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center transition-all" data-modal-hide="authentication-modal">
                          <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                          </svg>
                          <span class="sr-only">Close modal</span>
                      </button>
                  </div>
                  <!-- Modal body -->
                  <div class="p-4 md:p-5">

                      <!-- "action" here is the api endpoint for auth -->
                      <form on:submit={closeModal} class="space-y-4" action="api-end-point/">
                          <div>
                              <label for="email" class="block mb-2 text-sm font-medium text-gray-900 ">Your username:</label>
                              <input type="username" name="email" id="email" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="username" required>
                          </div>
                          <div>
                              <label for="password" class="block mb-2 text-sm font-medium text-gray-900 ">Your password:</label>
                              <input type="password" name="password" id="password" placeholder="••••••••" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " required>
                          </div>
                          <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Create Account</button>
                          <div class="text-sm font-medium text-gray-500 ">
                              Already have an account? <a href="#" class="text-primary-green hover:underline ">Sign In</a>
                          </div>
                      </form>
                  </div>
              </div>
          </div>
      </div> 

      </div>
  </div> 
{/if}
  
  <style>
    .modal {
      position: fixed;
      top: 0;
      bottom: 0;
      right: 0;
      left: 0;
      display: flex;
      justify-content: center;
      align-items: center;
  
      /* pointer-events: none; */
    }

  </style>

    <!-- 
    .contents {
      min-width: 240px;
      border-radius: 6px;
      padding: 16px;
      background: white;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      pointer-events: auto;
    }
  
    h2 {
      text-align: center;
      font-size: 24px;
    }
  
    p {
      text-align: center;
      margin-top: 16px;
    }
  
    .actions {
      margin-top: 32px;
      display: flex;
      justify-content: flex-end;
    } 
  
  </style> -->