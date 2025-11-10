using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private GameObject craftingPanel;
        [SerializeField] private Transform recipeButtonContainer;
        [SerializeField] private GameObject recipeButtonPrefab;
        [SerializeField] private TextMeshProUGUI stationNameText;
        
        [Header("Recipe Source")]
        private List<Recipe> _itemRecipes;
        private List<Recipe> _materialRecipes;
        private Transform _playerTransform;

        private CraftingStation _station;
        private Player Player => _playerTransform.GetComponent<Player>();

        void Awake()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (_playerTransform == null)
                Debug.LogError("Player not found. Make sure the Player GameObject is tagged 'Player'.");
            _itemRecipes = RecipeRegistry.AllRecipes["items"].FindAll(r => r.AllowedCraftingStations.Contains(stationType));
            _materialRecipes = RecipeRegistry.AllRecipes["materials"].FindAll(r => r.AllowedCraftingStations.Contains(stationType));
            Debug.Log($"Loaded {_itemRecipes.Count} item recipes and {_materialRecipes.Count} material recipes for {stationType}");
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
            return Vector2.Distance(transform.position, _playerTransform.position) <= interactionRange;
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

            CreateButtons(_materialRecipes);
            CreateButtons(_itemRecipes);
        }
        
        public void CloseCraftingUI()
        {
            Debug.LogWarning("Button pressed");
            if (craftingPanel != null)
                craftingPanel.SetActive(false);
        }
        
        private void CreateButtons(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.AllowedCraftingStations.Contains(stationType))
                {
                    var buttonGO = Instantiate(recipeButtonPrefab, recipeButtonContainer);
                    RecipeButtonBehaviour buttonBehaviour = buttonGO.GetComponent<RecipeButtonBehaviour>();
                    buttonBehaviour.Setup(recipe, this); // 'this' is the CraftingStationBehaviour
                    TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.text = recipe.Name;
                    Button button = buttonGO.GetComponent<Button>();
                    button.onClick.AddListener(() => buttonBehaviour.OnClick(recipe));;
                }
            }
        }

        private void OnRecipeSelected(Recipe recipe)
        {
            Debug.Log($"Selected recipe: {recipe.Name}");
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

        public static Recipe ConvertToRecipe(RecipeSO recipeSO)
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
                    recipeSO.outputMaterialType,
                    recipeSO.category,
                    ingredientDict,
                    recipeSO.allowedStations,
                    recipeSO.outputQuantity
                );
            }
        }
    }
}
