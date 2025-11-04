using System.Collections.Generic;
using Core;
using Enums;

namespace Gameplay
{
    public class Recipe
    {
        public CraftedItem OutputItem;
        public string Category;
        public Dictionary<IMaterial, int> Ingredients;
        public List<CraftingStationType> AllowedCraftingStations;
        private int _defaultOutputQuantity;

        public Recipe(CraftedItem outputItem, string category, Dictionary<IMaterial, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
        {
            OutputItem = outputItem;
            Category = category;
            Ingredients = ingredients;
            AllowedCraftingStations = allowedCraftingStations;
            _defaultOutputQuantity = defaultOutputQuantity;
        }
    }
}