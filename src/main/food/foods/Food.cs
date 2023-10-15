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
}