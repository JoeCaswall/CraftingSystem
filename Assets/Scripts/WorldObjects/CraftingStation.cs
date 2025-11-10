using System;
using Core;
using Enums;
using Gameplay;
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
            foreach (var kvp in recipe.Ingredients) // kvp.Key = material object, kvp.Value = requiredAmount
            {
                var requiredAmount = kvp.Value;
                var materialKey = kvp.Key.Name;
                var availableAmount = inventory.GetMaterialQuantity(materialKey);

                if (availableAmount < requiredAmount)
                {
                    Debug.Log($"Missing Material: {materialKey}. Required Amount: {requiredAmount}");
                    return false;
                }
            }

            return true;
        }

        // public CraftedItem Craft(ItemRecipe recipe, Player player)
        // {
        //     if (!IsCorrectStationType(recipe))
        //     {
        //         throw new InvalidOperationException($"Recipe {recipe.Name} cannot be crafted at this station.");
        //     }
        //
        //     if (!HasCorrectIngredients(player.Inventory, recipe))
        //     {
        //         throw new InvalidOperationException($"You do not have the correct ingredients.");
        //     }
        //     
        //     // Consume ingredients
        //     foreach (var (material, amount) in recipe.Ingredients)
        //     {
        //         player.Inventory.Materials[material] -= amount;
        //     }
        //
        //     // Add crafted output
        //     player.Inventory.AddItem(recipe.CraftedItem, recipe.DefaultOutputQuantity);
        //     return recipe.CraftedItem;
        // }

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
            
            foreach (var kvp in recipe.Ingredients)
            {
                var materialKey = kvp.Key.Name;
                var amount = kvp.Value;
                
                var current = player.Inventory.GetMaterialQuantity(materialKey);
                var newAmount = current - amount;
                player.Inventory.Materials[materialKey] = newAmount;
            }

            // Add crafted output (use the Inventory overload that accepts the runtime OutputMaterial)
            var output = recipe.GetOutputMaterial();
            player.Inventory.AddMaterial(output, recipe.DefaultOutputQuantity);
            return output;
        }
    }
}