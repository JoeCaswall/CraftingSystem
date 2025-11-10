using System.Collections.Generic;
using ScriptableObjects;
using Registries;
using UnityEngine;
using Gameplay;

namespace UnityScripts
{
    [DefaultExecutionOrder(-50)]
    public class RecipeLoader : MonoBehaviour
    {
        [SerializeField] private List<RecipeSO> recipeAssets;
        void Awake()
        {
            foreach (var recipeSO in recipeAssets)
            {
                Recipe runtimeRecipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);
        
                if (runtimeRecipe is ItemRecipe itemRecipe)
                    RecipeRegistry.RegisterItemRecipe(itemRecipe);
                else if (runtimeRecipe is MaterialRecipe materialRecipe)
                    RecipeRegistry.RegisterMaterialRecipe(materialRecipe);
            }
        }
    }
}