using System.Collections.Generic;
using UnityEngine;
using WorldObjects;
using Gameplay;
using TMPro;
using UnityEngine.UI;
using Enums;

namespace UnityScripts
{
    public class CraftingStationbehaviour : MonoBehaviour
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
        public List<Recipe> allRecipes;
        
        private Transform playerTransform;

        private CraftingStation _station;
        private Player Player => playerTransform.GetComponent<Player>();

        void Awake()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (playerTransform == null)
                Debug.LogError("Player not found. Make sure the Player GameObject is tagged 'Player'.");
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
            stationNameText.text = $"Crafting Station: {stationType}";

            // Clear old buttons
            foreach (Transform child in recipeButtonContainer)
                Destroy(child.gameObject);

            // Filter and create buttons
            foreach (var recipe in allRecipes)
            {
                if (recipe.AllowedCraftingStations.Contains(stationType))
                {
                    var buttonGO = Instantiate(recipeButtonPrefab, recipeButtonContainer);
                    var button = buttonGO.GetComponent<RecipeButtonBehaviour>();
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
    }
}
