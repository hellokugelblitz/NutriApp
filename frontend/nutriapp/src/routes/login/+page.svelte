<script>
    //Some auth stuff happens here
    import { fade } from 'svelte/transition'


</script>


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
                            Sign in to your account
                        </h3>
                    </div>
                    <!-- Modal body -->
                    <div class="p-4 md:p-5">
                        <!-- "action" here is the api endpoint for auth -->
                        <form class="space-y-4" action="/login">
                            <div>
                                <label for="email" class="block mb-2 text-sm font-medium text-gray-900 ">Your username:</label>
                                <input type="username" name="email" id="email" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="username" required>
                            </div>
                            <div>
                                <label for="password" class="block mb-2 text-sm font-medium text-gray-900 ">Your password:</label>
                                <input type="password" name="password" id="password" placeholder="••••••••" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " required>
                            </div>
                            <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Sign in to your account</button>
                            <div class="text-sm font-medium text-gray-500 ">
                                Not registered? <a href="/register" class="text-primary-green hover:underline ">Create account</a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div> 
    </div>
</div> 

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
    }
</style>

