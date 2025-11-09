using System.Collections.Generic;
using Core;
using Enums;

namespace Gameplay
{
    public class ItemRecipe : Recipe
    {
        public CraftedItem CraftedItem { get; }

        public ItemRecipe(string name, string category, Dictionary<IMaterial, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
            : base(name, category, ingredients, allowedCraftingStations, defaultOutputQuantity)
        {
            // Ensure all new recipes are added to the registry
            RecipeRegistry.RegisterItemRecipe(this);
        }
        
    }
}