using System.Collections.Generic;
using NutriApp.Food;

namespace NutriApp.Food
{
    //Shopping cart class
    public class ShoppingListController
    {
        //Keep a dictionary of users and their shopping lists
        private Dictionary<string, ShoppingList> shoppingLists = new Dictionary<string, ShoppingList>();

        private ShoppingListCriteria criteria;
        private FoodController foodController;

        //Constructors
        public ShoppingListController()
        {}
        public ShoppingListController(Dictionary<string, ShoppingList> lists)
        {
            this.shoppingLists = lists;
        }

        public ShoppingList GetShoppingList(string username) => shoppingLists[username];

        /// <summary>
        /// Update function is utilized by the food controller to initiate an update on the shopping list
        /// based on a specific criteria (criteria is defined inside of the ShoppingList class)
        /// </summary>
        public void Update(Recipe[] recipes, string username)
        {
            criteria.Update(recipes, username);
        }

        /// <summary>
        /// Set the current update criteria for the shopping list.
        /// </summary>
        public void SetCriteria(ShoppingListCriteria newCriteria)
        { 
            criteria = newCriteria; 
        }

        /// <summary>
        /// Add a specific amount of a specified ingredient to the shopping list
        /// </summary>
        public void AddItem(Ingredient ingredient, double amt, string username)
        {
            Dictionary<Ingredient, double> list = GetShoppingList(username).Entries;

            //If we cant find it in our list we add it with the amount
            if(!list.ContainsKey(ingredient))
            {
                list.Add(ingredient, amt);
                return;
            }

            //Else we are access what we already have and adding that amount
            list[ingredient] = list[ingredient] + amt;     
        }
        
        /// <summary>
        /// Remove a specific amount of a specified ingredient from the shopping list
        /// This method will not reduce the amount of an ingredient below 0
        /// </summary>
        public void RemoveItem(Ingredient ingredient, double amt, string username)
        {
            Dictionary<Ingredient, double> list = GetShoppingList(username).Entries;

            //There is nothing to remove to we exit
            if(!list.ContainsKey(ingredient))
                return;

            //Remove whatever we have
            list[ingredient] -= amt;

            //If we have zero remove it from the list entirely
            if(list[ingredient] <= 0)
                list.Remove(ingredient);
        }
        

        public void SetList(Dictionary<Ingredient, double> newList, string username)
        {
            GetShoppingList(username).Entries = newList;
        }
    }

    //Strategy interface
    public interface ShoppingListCriteria {
        public void Update(Recipe[] recipes, string username);
    }

    public class SpecificRecipeCriteria : ShoppingListCriteria 
    {

        private FoodController foodController;
        private ShoppingListController shoppingList;

        public SpecificRecipeCriteria(ShoppingListController list, FoodController foodController)
        {
            shoppingList = list;
            this.foodController = foodController;
        }

        public void Update(Recipe[] recipes, string username) 
        { 
            //Here I am making a copy of the original shoppingList class list so that we can set it later.
            Dictionary<Ingredient, double> newList = shoppingList.GetShoppingList(username).Entries;
            Dictionary<Ingredient, double> minimumIngredientRequirements = new Dictionary<Ingredient, double>();

            foreach (Recipe recipe in recipes)
            {
                foreach (Ingredient ingredient in recipe.Children.Keys)
                {
                    double recipeRequirement = recipe.Children[ingredient];

                    if (!minimumIngredientRequirements.ContainsKey(ingredient))
                        minimumIngredientRequirements.Add(ingredient, recipeRequirement);
                    else
                    {
                        if (recipeRequirement < minimumIngredientRequirements[ingredient])
                            minimumIngredientRequirements[ingredient] = recipeRequirement;
                        else continue;
                    }

                    if (foodController.GetSingleIngredientStock(ingredient.Name, username) >= recipeRequirement)
                        continue;

                    if(!newList.ContainsKey(ingredient))
                        newList.Add(ingredient, recipeRequirement - foodController.GetSingleIngredientStock(ingredient.Name, username));
                }
            }

            //After we have made the changes we want we can save it to the shoppingList.
            shoppingList.SetList(newList, username);
        }
    }

    //Here we define any future criteria we want to.
}

public class ShoppingList
{
    public Dictionary<Ingredient, double> Entries {get; set;}

    public ShoppingList()
    {
        Entries = new Dictionary<Ingredient, double>();
    }
}