using System.Collections.Generic;
using Core;
using UnityEngine;
using WorldObjects;
using Gameplay;
using TMPro;
using UnityEngine.UI;
using Enums;
using ScriptableObjects;
using UI;

namespace UnityScripts
{
    public class CraftingStationBehaviour : MonoBehaviour
    {
        [Header("Station Setup")]
        public CraftingStationType stationType;
        public float interactionRange = 1.5f;
        public string stationName;
        
        [Header("UI References")]
        public GameObject craftingPanel;
        public TextMeshProUGUI stationNameText;
        public Transform recipeButtonContainer;
        public GameObject recipeButtonPrefab;
        
        [Header("Recipe Source")]
        public List<Recipe> itemRecipes;
        public List<Recipe> materialRecipes;
        private Transform playerTransform;

        private CraftingStation _station;
        private Player Player => playerTransform.GetComponent<Player>();

        void Awake()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (playerTransform == null)
                Debug.LogError("Player not found. Make sure the Player GameObject is tagged 'Player'.");
            itemRecipes = RecipeRegistry.AllRecipes["items"].FindAll(r => r.AllowedCraftingStations.Contains(stationType));
            materialRecipes = RecipeRegistry.AllRecipes["materials"].FindAll(r => r.AllowedCraftingStations.Contains(stationType));
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange())
            {
                OpenCraftingUI();
            }
        }
        
        bool IsPlayerInRange()
        {
            return Vector2.Distance(transform.position, playerTransform.position) <= interactionRange;
        }

        void OpenCraftingUI()
        {
            if (craftingPanel == null || recipeButtonContainer == null || recipeButtonPrefab == null)
            {
                Debug.LogWarning("Crafting UI references are missing.");
                return;
            }

            craftingPanel.SetActive(true);
            stationNameText.text = stationType.ToString();

            // Clear old buttons
            foreach (Transform child in recipeButtonContainer)
                Destroy(child.gameObject);

            CreateButtons(materialRecipes);
            CreateButtons(itemRecipes);
        }


        private void CreateButtons(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.AllowedCraftingStations.Contains(stationType))
                {
                    var buttonGo = Instantiate(recipeButtonPrefab, recipeButtonContainer);
                    var button = buttonGo.GetComponent<RecipeButtonBehaviour>();
                    button.Setup(recipe, this);
                }
            }
        }

        public void TryCraft(Recipe recipe)
        {
            if (recipe is ItemRecipe itemRecipe)
            {
                var result = _station.Craft(itemRecipe, Player);
                Debug.Log($"Crafted item: {result.Name}");
            }
            else if (recipe is MaterialRecipe materialRecipe)
            {
                var result = _station.Craft(materialRecipe, Player);
                Debug.Log($"Refined material: {result.Name}");
            }
            else
            {
                Debug.LogWarning("Unknown recipe type.");
            }
        }

        public Recipe ConvertToRecipe(RecipeSO recipeSO)
        {
            var ingredientDict = new Dictionary<IMaterial, int>();
            foreach (var ingredient in recipeSO.ingredients)
            {
                ingredientDict.Add(new MaterialWrapper(ingredient.material), ingredient.quantity);
            }

            if (recipeSO.isItemRecipe)
            {
                return new ItemRecipe(
                    recipeSO.recipeName,
                    recipeSO.category,
                    ingredientDict,
                    recipeSO.allowedStations,
                    recipeSO.outputQuantity
                );
            }
            else
            {
                return new MaterialRecipe(
                    recipeSO.recipeName,
                    recipeSO.OutputMaterialType,
                    recipeSO.category,
                    ingredientDict,
                    recipeSO.allowedStations,
                    recipeSO.outputQuantity
                );
            }
        }
    }
}
