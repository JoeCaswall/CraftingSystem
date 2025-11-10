using System.Collections.Generic;

namespace Gameplay
{
    public static class RecipeRegistry
    {
        public static Dictionary<string, List<Recipe>> AllRecipes { get; } = new Dictionary<string, List<Recipe>>
        {
            { "items", new List<Recipe>() },
            { "materials", new List<Recipe>() }
        };
        
        public static void RegisterItemRecipe(Recipe recipe)
        {
            if (!AllRecipes["items"].Contains(recipe))
                AllRecipes["items"].Add(recipe);
        }

        public static void RegisterMaterialRecipe(Recipe recipe)
        {
            if (!AllRecipes["materials"].Contains(recipe))
                AllRecipes["materials"].Add(recipe);
        }
    }
}