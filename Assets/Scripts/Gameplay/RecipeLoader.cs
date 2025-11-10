using System.Collections.Generic;
using ScriptableObjects;
using Registries;
using UnityEngine;
using UnityScripts;

namespace Gameplay
{
    [DefaultExecutionOrder(-100)]
    public class RecipeLoader : MonoBehaviour
    {
        [SerializeField] private List<RecipeSO> recipeAssets;
        void Awake()
        {
            foreach (var recipeSO in recipeAssets)
            {
                Recipe runtimeRecipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);
                Debug.Log(runtimeRecipe.Name);
        
                if (runtimeRecipe is ItemRecipe itemRecipe)
                    RecipeRegistry.RegisterItemRecipe(itemRecipe);
                else if (runtimeRecipe is MaterialRecipe materialRecipe)
                    RecipeRegistry.RegisterMaterialRecipe(materialRecipe);
            }
        }
    }
}