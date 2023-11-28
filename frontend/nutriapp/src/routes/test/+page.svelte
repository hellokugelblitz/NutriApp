<script lang="ts">
  import { onMount } from 'svelte';

  // Define a type for the meal data
  interface Meal {
    Name: string;
    // Add other properties as needed
  }

  export let meals: Meal[] = [];

  // Fetch meals from the API
  async function fetchMeals() {
  try {
    const sessionToken = 'sessionKey'; // Replace with the actual session token

    const response = await fetch('http://localhost:5072/api/Meals', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'NutriAppScheme': sessionToken, // Include the session header
      },
    });

    const data = await response.json();
    meals = data;
  } catch (error) {
    console.error('Error fetching meals:', error);
  }
}

  // Fetch meals when the component is mounted
  onMount(() => {
    fetchMeals();
  });
</script>

  <h1>Meals</h1>
  {#if meals.length > 0}
    <ul>
      {#each meals as meal (meal.Name)}
        <li>{meal.Name}</li>
      {/each}
    </ul>
  {:else}
    <p>No meals available.</p>
  {/if}

  