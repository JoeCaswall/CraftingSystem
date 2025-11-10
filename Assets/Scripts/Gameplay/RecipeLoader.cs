using System.Collections.Generic;
using ScriptableObjects;
using Registries;
using UnityEngine;
using UnityScripts;

namespace Gameplay
{
    [DefaultExecutionOrder(-50)]
    public class RecipeLoader : MonoBehaviour
    {
        [SerializeField] private List<RecipeSO> recipeAssets;
        void Awake()
        {
            foreach (var recipeSO in recipeAssets)
            {
            Debug.Log($"RecipeSO {recipeSO.recipeName} ingredients count: {recipeSO.ingredients?.Count ?? 0}");
                Recipe runtimeRecipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);
                Debug.Log("#######################################");
                Debug.Log(runtimeRecipe.Name);
                Debug.Log("#######################################");
        
                if (runtimeRecipe is ItemRecipe itemRecipe)
                    RecipeRegistry.RegisterItemRecipe(itemRecipe);
                else if (runtimeRecipe is MaterialRecipe materialRecipe)
                    RecipeRegistry.RegisterMaterialRecipe(materialRecipe);
            }
        }
    }
}