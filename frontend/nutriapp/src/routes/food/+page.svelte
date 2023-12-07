<script lang="ts">
    import { page } from "$app/stores";
    import Nav from "$lib/ui/Nav.svelte";
    import { SvelteToast, toast } from "@zerodevx/svelte-toast";

    let activeButton = "";
    let exit = false;
    let mealName = "";

    function sendToast(message: string) {
        toast.push(message);
        resetAfterDelay();
    }

    function resetAfterDelay() {
        setTimeout(() => {
            exit = false;
            activeButton = "";
            mealName = "";
        }, 1000);
    }
</script>

<Nav title="Food" current_data={$page.data} />

<div class="p-4 md:ml-64 mx-0 max-w-6xl">
    {#if activeButton == ""}
        {#if !$page.data.user}
            <div class="grid md:grid-cols-3 gap-4 text-center">
                <!-- Ingredients Section -->
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Ingredients</h1>
                    <ul>
                        {#each $page.data.ingredients as ingredient}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {ingredient.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
                <!-- Recipes Section -->
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Recipes</h1>
                    <ul>
                        {#each $page.data.recipes as recipe}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {recipe.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
                <!-- Meals Section -->
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Meals</h1>
                    <ul>
                        {#each $page.data.meals as meal}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {meal.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
            </div>
        {:else}
            <div class="flex flex-wrap justify-center gap-4 mb-4">
                <button
                    class="bg-dark-green hover:bg-dark-dark-green text-white py-2 px-4 rounded"
                    on:click={() => (activeButton = "consume")}
                    >Consume Meal</button
                >
                <button
                    class="bg-dark-green hover:bg-dark-dark-green text-white py-2 px-4 rounded"
                    on:click={() => (activeButton = "recipe")}
                    >Create Recipe</button
                >
                <button
                    class="bg-dark-green hover:bg-dark-dark-green text-white py-2 px-4 rounded"
                    on:click={() => (activeButton = "meal")}>Create Meal</button
                >
                <button
                    class="bg-dark-green hover:bg-dark-dark-green text-white py-2 px-4 rounded"
                    on:click={() => (activeButton = "shopping")}
                    >View Shopping Cart</button
                >
            </div>
            <div class="grid md:grid-cols-3 gap-4 text-center">
                <!-- Ingredients Section -->
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Ingredients</h1>
                    <ul>
                        {#each $page.data.ingredients as ingredient}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {ingredient.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
                <!-- Recipes Section -->
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Recipes</h1>
                    <ul>
                        {#each $page.data.recipes as recipe}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {recipe.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
                <!-- Meals Section -->
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Meals</h1>
                    <ul>
                        {#each $page.data.meals as meal}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {meal.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
            </div>
        {/if}
    {:else if activeButton == "consume"}
        {#if (exit = true)}
            <div class="grid md:grid-cols-2 gap-4">
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Consume a Meal</h1>

                    <form action="?/consume" method="POST">
                        <div>
                            <label for="meal" class="text-gray-700 font-bold"
                                >Meal Name</label
                            >
                            <input
                                bind:value={mealName}
                                type="text"
                                name="mealName"
                                id="mealName"
                                class="border-2 border-black rounded-md p-2 ml-2"
                                placeholder="Meal Name"
                                required
                            />
                        </div>

                        <button
                            class="bg-dark-green hover:bg-dark-dark-green text-white py-2 px-4 rounded float-right"
                            on:click={() => {
                                if (mealName != "") {
                                    sendToast("Consumed Meal: " + mealName);
                                }
                            }}>Submit</button
                        >
                    </form>
                </div>
                <div class="p-4 shadow-lg rounded-lg bg-white">
                    <h1 class="text-2xl font-bold mb-4">Meals</h1>
                    <ul>
                        {#each $page.data.meals as meal}
                            <li>
                                <button
                                    class="text-left w-full hover:bg-gray-100 p-2 rounded transition duration-200 ease-in-out"
                                >
                                    {meal.name}
                                </button>
                            </li>
                        {/each}
                    </ul>
                </div>
            </div>
            <button
                class="bg-dark-green hover:bg-dark-dark-green text-white py-2 px-4 rounded mt-4 float-right"
                on:click={() => (activeButton = "")}>Back</button
            >
        {/if}
    {:else if activeButton == "recipe"}
        <div>test recipe</div>
    {:else if activeButton == "meal"}
        <div>test meal</div>
    {:else if activeButton == "shopping"}
        <div>test shopping</div>
    {/if}
</div>
