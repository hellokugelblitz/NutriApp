using System.Collections.Generic;

namespace NutriApp.Food;

/// <summary>
/// A food type that consists of another type of food (specified by T).
/// </summary>
public abstract class PreparedFood<T> : Food where T : Food
{
    private string name;
    private Dictionary<T, double> children;

    public Dictionary<T, double> Children => children;
    public string Name => name;

    /// <summary>
    /// Returns a dictionary where the key is an ingredient, and the value is
    /// the amount of that ingredient required to prepare this food.
    /// </summary>
    public Dictionary<Ingredient, double> Ingredients { get; }
    
    public double Calories
    {
        get
        {
            double result = 0.0;
            foreach (T child in children.Keys)
                result += child.Calories;

            return result;
        }
    }

    public double Fat
    {
        get
        {
            double result = 0.0;
            foreach (T child in children.Keys)
                result += child.Fat;

            return result;
        }
    }

    public double Protein
    {
        get
        {
            double result = 0.0;
            foreach (T child in children.Keys)
                result += child.Protein;

            return result;
        }
    }

    public double Fiber
    {
        get
        {
            double result = 0.0;
            foreach (T child in children.Keys)
                result += child.Fiber;

            return result;
        }
    }

    public double Carbohydrates
    {
        get
        {
            double result = 0.0;
            foreach (T child in children.Keys)
                result += child.Carbohydrates;

            return result;
        }
    }

    public PreparedFood(string name)
    {
        this.name = name;
    }

    public void AddChild(T child, double quantity) => children.Add(child, quantity);

    public void RemoveChild(T child) => children.Remove(child);

    public override string ToString() => name;
}