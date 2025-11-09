using UnityEngine;
using Enums;
using Gameplay;
using Core;
using System.Collections.Generic;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Crafting/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        public string recipeName;
        public string category;
        public List<MaterialQuantity> ingredients;
        public List<CraftingStationType> allowedStations;
        public int outputQuantity;
        public bool isItemRecipe; // true = item, false = material
        public OutputMaterialType OutputMaterialType;
    }
}