using System;
using System.Collections.Generic;
using Core;
using Enums;

namespace Gameplay
{
    public class Recipe //TODO: Add materialrecipe and itemrecipe classes which extend this to allow for easier crafting table logic
    {
        private static HashSet<string> _existingNames = new();
        public string Name {get; set;}
        public string Category { get; }
        public Dictionary<IMaterial, int> Ingredients { get; }
        public List<CraftingStationType> AllowedCraftingStations { get; }
        public int DefaultOutputQuantity { get; private set; }

        protected Recipe(string name, string category, Dictionary<IMaterial, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
        {
            Name = name;
            Category = category;
            Ingredients = ingredients;
            AllowedCraftingStations = allowedCraftingStations;
            DefaultOutputQuantity = defaultOutputQuantity;
            
            // Check if a recipe exists with this name so that RecipeBook can use names as unique keys
            if (_existingNames.Contains(name))
                throw new ArgumentException($"Recipe name '{name}' already exists.");
            
            _existingNames.Add(name);
        }
    }
}