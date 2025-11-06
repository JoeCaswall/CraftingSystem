using System.Collections.Generic;
using Core;
using Enums;

namespace Gameplay
{
    public class MaterialRecipe : Recipe
    {
        public OutputMaterial OutputMaterial { get; }
        public MaterialRecipe(OutputMaterial outputMaterial, string name, string category, Dictionary<IMaterial, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
            : base(name, category, ingredients, allowedCraftingStations, defaultOutputQuantity)
        {
            OutputMaterial = outputMaterial;
        }
    }
}