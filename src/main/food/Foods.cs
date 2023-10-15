using System.Collections.Generic;

namespace NutriApp.Food
{
    public interface Food
    {
        public string Name { get; }
        public double Calories { get; }
        public double Fat { get; }
        public double Protein { get; }
        public double Fiber { get; }
        public double Carbohydrates { get; }
    }

    public abstract class PreparedFood<T> : Food where T : Food
    {
        private string name;
        private Dictionary<T, double> children;

        public Dictionary<T, double> Children { get; }
        public Dictionary<T, double> Ingredients { get; }
        public string Name { get; }
        public double Calories { get; }
        public double Fat { get; }
        public double Protein { get; }
        public double Fiber { get; }
        public double Carbohydrates { get; }

        public void AddChild(T child, double quantity) {}

        public void RemoveChild(T child) {}
    }

    public class Ingredient : Food
    {
        private string name;
        private double stock;

        public double Stock { get; set; }
        public string Name { get; }
        public double Calories { get; }
        public double Fat { get; }
        public double Protein { get; }
        public double Fiber { get; }
        public double Carbohydrates { get; }
    }

    public class Recipe : PreparedFood<Ingredient>
    {
        private List<string> instructions;

        public string[] Instructions { get; }

        public void AddInstruction(string step) {}
    }

    public class Meal : PreparedFood<Recipe> { }
}