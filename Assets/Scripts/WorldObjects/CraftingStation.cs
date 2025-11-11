using System;
using Core;
using Enums;
using Gameplay;
using Registries;
using UnityEngine;

namespace WorldObjects
{
    public class CraftingStation
    {
        private string _name;
        private CraftingStationType _type;

        public CraftingStation(string name, CraftingStationType type)
        {
            _name = name;
            _type = type;
        }
        private bool IsCorrectStationType(Recipe recipe)
        {
            return recipe.AllowedCraftingStations.Contains(_type);
        }

        private bool HasCorrectIngredients(Inventory inventory, Recipe recipe)
        {
            Debug.Log($"HasCorrectIngredients called for recipe: {recipe?.Name} Ingredients count: {recipe?.Ingredients?.Count ?? 0}");
            foreach (var kvp in recipe.Ingredients) // kvp.Key = material object, kvp.Value = requiredAmount
            {
                var requiredAmount = kvp.Value;
                var materialKey = kvp.Key;
                var availableAmount = inventory.GetMaterialQuantity(materialKey);
                
                Debug.Log($"Checking material '{materialKey}' required:{requiredAmount} available:{availableAmount}");
                
                if (availableAmount < requiredAmount)
                {
                    Debug.Log($"Missing Material: {materialKey}. Required Amount: {requiredAmount}");
                    return false;
                }
            }
            
            Debug.Log("HasCorrectIngredients => true");
            return true;
        }

        public OutputMaterial Craft(MaterialRecipe recipe, Player player)
        {
            if (!IsCorrectStationType(recipe))
            {
                throw new InvalidOperationException($"Recipe {recipe.Name} cannot be crafted at this station.");
            }

            if (!HasCorrectIngredients(player.Inventory, recipe))
            {
                throw new InvalidOperationException($"You do not have the correct ingredients.");
            }

            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (recipe == null) 
                throw new ArgumentNullException(nameof(recipe));
            
            foreach (var kvp in recipe.Ingredients)
            {
                var materialKey = kvp.Key;
                var amount = kvp.Value;
                
                var current = player.Inventory.GetMaterialQuantity(materialKey);
                var newAmount = current - amount;
                player.Inventory.Materials[materialKey] = newAmount;
                
                //TODO: This broke the removing logic - implement properly later as would allow Materials to be a private field
                // player.Inventory.RemoveMaterial(MaterialRegistry.Get(materialKey), amount); 
            }

            // Add crafted output (use the Inventory overload that accepts the runtime OutputMaterial)
            var output = recipe.GetOutputMaterial();
            player.Inventory.AddMaterial(output, recipe.DefaultOutputQuantity);
            return output;
        }
    }
}