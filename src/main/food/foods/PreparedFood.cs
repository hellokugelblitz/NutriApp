using System.Collections.Generic;

namespace NutriApp.Food
{
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
}