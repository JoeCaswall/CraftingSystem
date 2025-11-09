using System.Collections.Generic;
using System.Linq;
using Core;
using Enums;
using UnityEngine.Rendering;

namespace Gameplay
{
    public class MaterialRecipe : Recipe
    {
        public OutputMaterialType MaterialType;
        public MaterialRecipe(string name, OutputMaterialType type, string category, Dictionary<IMaterial, int> ingredients,
            List<CraftingStationType> allowedCraftingStations, int defaultOutputQuantity)
            : base(name, category, ingredients, allowedCraftingStations, defaultOutputQuantity)
        {
            MaterialType = type;
            // // Ensure all new recipes are added to the registry
            RecipeRegistry.RegisterMaterialRecipe(this);
        }

        public OutputMaterial GetOutputMaterial()
        {
            return new OutputMaterial(
                name: this.Name,
                type: this.MaterialType
            );
        }
    }
}