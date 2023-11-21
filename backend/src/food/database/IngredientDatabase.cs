using System.Collections.Generic;

namespace NutriApp.Food;

public interface IngredientDatabase
{
    public void Add(Ingredient ingredient);
    public void Remove(Ingredient ingredient);
    public Ingredient Get(string name);
    public Ingredient[] GetAll();
    public Ingredient[] Search(string term);
}