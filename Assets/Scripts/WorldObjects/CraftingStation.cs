using System;
using Core;
using Enums;
using Gameplay;

namespace WorldObjects
{
    public class CraftingStation
    {
        private string _name;
        private CraftingStationType _type;

        private bool IsCorrectStationType(Recipe recipe)
        {
            return recipe.AllowedCraftingStations.Contains(_type);
        }

        private bool HasCorrectIngredients(Inventory inventory, Recipe recipe)
        {
            // Early return false if key is missing or amount in inventory is less than required amount
            foreach (var (requiredMaterial, requiredAmount) in recipe.Ingredients)
            {
                if (!inventory.Materials.TryGetValue(requiredMaterial, out var availableAmount) ||
                    availableAmount < requiredAmount)
                {
                    return false;
                }
            }
            return true;
        }

        public CraftedItem Craft(ItemRecipe recipe, Player player)
        {
            if (!IsCorrectStationType(recipe))
            {
                throw new InvalidOperationException($"Recipe {recipe.Name} cannot be crafted at this station.");
            }

            if (!HasCorrectIngredients(player.Inventory, recipe))
            {
                throw new InvalidOperationException($"You do not have the correct ingredients.");
            }
            
            // Consume ingredients
            foreach (var (material, amount) in recipe.Ingredients)
            {
                player.Inventory.Materials[material] -= amount;
            }

            // Add crafted output
            player.Inventory.AddItem(recipe.CraftedItem, recipe.DefaultOutputQuantity);
            return recipe.CraftedItem;
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
            
            // Consume ingredients
            foreach (var (material, amount) in recipe.Ingredients)
            {
                player.Inventory.Materials[material] -= amount;
            }

            // Add crafted output
            player.Inventory.AddMaterial(recipe.OutputMaterial, 1); //TODO: write inventory methods
            return recipe.OutputMaterial;
        }
    }
}