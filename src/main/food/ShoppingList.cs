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
        public ShoppingCart(){}
        public ShoppingCart(Dictionary<Ingredient, double> list){}

        //Other methods
        public void Update()
        {
            return this.criteria.Update();
        }
        public void SetCriteria(ShoppingListCriteria newCriteria){ this.criteria = newCriteria; }
        public void AddItemStock(){};
        public void RemoveItemStock(){};
    }

    //Strategy interface
    public interface ShoppingListCriteria {
        public void Update();
    }

    public class SpecificRecipeCriteria : ShoppingListCriteria {
        public void Update() 
        { 
            //TODO: CONCRETE IMPLEMENTATION
        }
    }

    //Here we define any future criteria we want to.

}