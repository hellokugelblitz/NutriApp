<script lang="ts">
    //Some auth stuff happens here
    import { onMount } from 'svelte';
    import { setContext } from 'svelte';
    import { goto } from '$app/navigation';

    //Isnt really necessary
    let username = '';
    let password = '';
    let height = 0;
    let birthday = "2017-11-01T00:00:00";
    let name = '';
    let currentWeight = 20.00;
    let weightGoal = 40.00;
</script>

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
                            Create a new account
                        </h3>
                    </div>
                    <!-- Modal body -->
                    <div class="p-4 md:p-5">
                        <!-- "action" here is the api endpoint for auth -->
                        <form class="space-y-4" action="?/register" method="POST">
                            <div>
                                <label for="username" class="block mb-2 text-sm font-medium text-gray-900 ">Your username:</label>
                                <input bind:value={username} type="username" name="username" id="username" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="username" required>
                            </div>
                            <div>
                                <label for="password" class="block mb-2 text-sm font-medium text-gray-900 ">Your password:</label>
                                <input bind:value={password} type="password" name="password" id="password" placeholder="••••••••" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " required>
                            </div>
                            <div>
                                <label for="name" class="block mb-2 text-sm font-medium text-gray-900 ">Your name:</label>
                                <input bind:value={name} type="name" name="name" id="name" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="John Smith" required>
                            </div>
                            <div class="flex flex-row w-full gap-4">
                                <div>
                                    <label for="name" class="block mb-2 text-sm font-medium text-gray-900 ">Your height:</label>
                                    <input bind:value={name} type="name" name="name" id="name" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="5'11" required>
                                </div>
                                <div>
                                    <label for="name" class="block mb-2 text-sm font-medium text-gray-900 ">Birth Date:</label>
                                    <input class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5" type="date" id="start" name="trip-start" value="2018-07-22" min="2018-01-01" max="2018-12-31" />
                                </div>
                            </div>
                            <div class="flex flex-row gap-4">
                                <div>
                                    <label for="weight" class="block mb-2 text-sm font-medium text-gray-900 ">Current Weight:</label>
                                    <input bind:value={currentWeight} type="number" name="weight" id="weight" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="200" max="500" required>
                                </div>
                                <div>
                                    <label for="goal" class="block mb-2 text-sm font-medium text-gray-900 ">Current Goal:</label>
                                    <input bind:value={weightGoal} type="number" name="goal" id="goal" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-green focus:border-primary-green block w-full p-2.5 " placeholder="150" min="90" required>
                                </div>
                            </div>

         

                            <button type="submit" class="w-full text-white bg-dark-green mt-6 hover:bg-primary-green focus:ring-4 focus:outline-none focus:ring-light-green font-medium rounded-lg text-sm px-5 py-2.5 text-center transition-all">Create your account</button>
                            <div class="text-sm font-medium text-gray-500 ">
                                Already have an account? <a href="/login" class="text-primary-green hover:underline ">Sign in</a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div> 
    </div>
</div> 

