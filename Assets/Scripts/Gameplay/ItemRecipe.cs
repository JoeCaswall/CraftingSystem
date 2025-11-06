using System.Collections.Generic;
using Core;
using Enums;

namespace Gameplay
{
    public class ItemRecipe : Recipe
    {
        public CraftedItem CraftedItem { get; }

        public ItemRecipe(CraftedItem craftedItem, string name, string category, Dictionary<IMaterial, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
            : base(name, category, ingredients, allowedCraftingStations, defaultOutputQuantity)
        {
            CraftedItem = craftedItem;
        }
        
    }
}