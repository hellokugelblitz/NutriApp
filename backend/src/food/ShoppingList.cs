using System.Collections.Generic;

namespace NutriApp.Food
{
    //Shopping cart class
    public class ShoppingList
    {
        //Keep a dictionary of ingredients
        private Dictionary<Ingredient, double> list = new Dictionary<Ingredient, double>();
        
        /// <summary>
        /// Retrieves the current list.
        /// </summary>
        public Dictionary<Ingredient, double> List => list;

        private ShoppingListCriteria criteria;

        //Constructors
        public ShoppingList(){}
        public ShoppingList(Dictionary<Ingredient, double> list)
        {
            this.list = list;
        }

        /// <summary>
        /// Update function is utilized by the food controller to initiate an update on the shopping list
        /// based on a specific criteria (criteria is defined inside of the ShoppingList class)
        /// </summary>
        public void Update(Recipe[] recipes)
        {
            criteria.Update(recipes);
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
        public void AddItem(Ingredient ingredient, double amt)
        {
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
        public void RemoveItem(Ingredient ingredient, double amt)
        {
            //There is nothing to remove to we exit
            if(!list.ContainsKey(ingredient))
                return;

            //Remove whatever we have
            list[ingredient] -= amt;

            //If we have zero remove it from the list entirely
            if(list[ingredient] <= 0)
                list.Remove(ingredient);
        }
        

        public void SetList(Dictionary<Ingredient, double> newList)
        {
            list = newList;
        }
    }

    //Strategy interface
    public interface ShoppingListCriteria {
        public void Update(Recipe[] recipes);
    }

    public class SpecificRecipeCriteria : ShoppingListCriteria {

        private ShoppingList shoppingList;

        public SpecificRecipeCriteria(ShoppingList list){
            shoppingList = list;
        }

        public void Update(Recipe[] recipes) 
        { 
            //Here I am making a copy of the original shoppingList class list so that we can set it later.
            Dictionary<Ingredient, double> newList = shoppingList.List;
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

                    if (ingredient.Stock >= recipeRequirement)
                        continue;

                    if(!newList.ContainsKey(ingredient))
                        newList.Add(ingredient, recipeRequirement - ingredient.Stock);
                }
            }

            //After we have made the changes we want we can save it to the shoppingList.
            shoppingList.SetList(newList);
        }
    }

    //Here we define any future criteria we want to.
}