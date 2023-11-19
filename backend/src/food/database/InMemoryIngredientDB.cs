using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace NutriApp.Food;

public class InMemoryIngredientDatabase : IngredientDatabase
{
    private const string INGREDIENTS_CSV_PATH = "ingredients.csv";
    private const int NAME_INDEX = 1;
    private const int CALORIES_INDEX = 3;
    private const int PROTEIN_INDEX = 4;
    private const int FAT_INDEX = 5;
    private const int CARBOHYDRATES_INDEX = 7;
    private const int FIBER_INDEX = 8;

    private HashSet<Ingredient> ingredients;
    
    public InMemoryIngredientDatabase()
    {
        ingredients = new HashSet<Ingredient>();

        // Read from ingredients CSV and load each entry into memory as a
        // new Ingredient instance.
        TextFieldParser parser = new TextFieldParser(INGREDIENTS_CSV_PATH)
        {
            Delimiters = new string[] { "," },
            HasFieldsEnclosedInQuotes = true,
            TrimWhiteSpace = true
        };

        string[] fields;

        while (!parser.EndOfData)
        {
            fields = parser.ReadFields();
        
            try
            {
                ingredients.Add(new Ingredient(
                fields[NAME_INDEX],
                    fields[CALORIES_INDEX] == "" ? 0.0 : double.Parse(fields[CALORIES_INDEX]),  // Some fields are blank, which will cause
                    fields[FAT_INDEX] == "" ? 0.0 : double.Parse(fields[FAT_INDEX]),            // double.Parse to freak out. Fill in 0 for these
                    fields[PROTEIN_INDEX] == "" ? 0.0 : double.Parse(fields[PROTEIN_INDEX]),    // fields.
                    fields[FIBER_INDEX] == "" ? 0.0 : double.Parse(fields[FIBER_INDEX]),
                    fields[CARBOHYDRATES_INDEX] == "" ? 0.0 : double.Parse(fields[CARBOHYDRATES_INDEX])
                ));
            }
            catch (System.FormatException) { continue; }  // Ignore header row, which will not be formatted correctly
        }

        parser.Close();
    }

    public void Add(Ingredient ingredient) => ingredients.Add(ingredient);
    
    public void Remove(Ingredient ingredient) => ingredients.Remove(ingredient);

    public Ingredient Get(string name)
    {
        foreach (Ingredient ingredient in ingredients)
            if (ingredient.Name.ToLower() == name.ToLower().Trim())
                return ingredient;

        return null;
    }

    public Ingredient[] GetAll()
    {
        Ingredient[] result = new Ingredient[ingredients.Count];
        ingredients.CopyTo(result);
        return result;
    }

    public Ingredient[] Search(string term)
    {
        List<Ingredient> matches = new List<Ingredient>();

        foreach (Ingredient ingredient in ingredients)
            if (ingredient.Name.ToLower().Contains(term.ToLower().Trim()))
                matches.Add(ingredient);

        return matches.ToArray();
    }
}