using System.Collections.Generic;
using Core;
using Enums;
using Registries;

namespace Gameplay
{
    public class ItemRecipe : Recipe
    {
        public CraftedItem CraftedItem { get; }

        public ItemRecipe(string name, string category, Dictionary<string, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
            : base(name, category, ingredients, allowedCraftingStations, defaultOutputQuantity)
        {
            // Ensure all new recipes are added to the registry
            RecipeRegistry.RegisterItemRecipe(this);
        }
        
    }
}