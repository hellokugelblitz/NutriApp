using System.Collections.Generic;

namespace NutriApp.Food
{
    //Shopping cart class
    public class ShoppingList
    {
        //Keep a dictionary of ingredients
        private Dictionary<Ingredient, double> list = new Dictionary<Ingredient, double>();
        private ShoppingListCriteria criteria;

        //Constructors
        public ShoppingList(){}
        public ShoppingList(Dictionary<Ingredient, double> list)
        {
            this.list = list;
        }

        //Other methods
        public void Update(Recipe recipe)
        {
            criteria.Update(recipe);
        }
        public void SetCriteria(ShoppingListCriteria newCriteria)
        { 
            criteria = newCriteria; 
        }
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
        public void RemoveItem(Ingredient ingredient, double amt)
        {
            //There is nothing to remove to we exit
            if(!list.ContainsKey(ingredient))
                return;

            //Remove whatever we have
            if(list[ingredient] >= amt)
                list[ingredient] -= amt;

            //If we have zero remove it from the list entirely
            if(list[ingredient] <= 0)
                list.Remove(ingredient);
        }
    }

    //Strategy interface
    public interface ShoppingListCriteria {
        public void Update(Recipe recipe);
    }

    public class SpecificRecipeCriteria : ShoppingListCriteria {
        public void Update(Recipe recipe) 
        { 
            foreach (var item in recipe.Children)
            {
                //if the item.key exists inside of our shopping list dict
                    //If the current value of  isnt larger 
                        //we update it to the minimum
                //else
                    //Enter that item into the list under new key
                    //Set that to the min
            }
        }
    }

    //Here we define any future criteria we want to.
}