using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Enums;
using Gameplay;
using Registries;
using ScriptableObjects;
using UnityScripts;

namespace Editor
{
    [TestFixture]
    public class ConvertToRecipeTests
    {
        private MaterialSO _tin;
        private MaterialSO _copper;
        
        [SetUp]
        public void Setup()
        {
            MaterialRegistry.Clear();
            // Register all raw materials used in tests
            MaterialRegistry.Register(new RawMaterial("Flax"));
            MaterialRegistry.Register(new RawMaterial("TinOre"));
            MaterialRegistry.Register(new RawMaterial("Copper"));
            _tin = ScriptableObject.CreateInstance<MaterialSO>();
            _copper = ScriptableObject.CreateInstance<MaterialSO>();

            _tin.materialName = "TinOre";
            _tin.outputMaterialType = OutputMaterialType.MetalBar;
            _tin.rawMaterialType = RawMaterialType.MetalOre;
            _tin.isRawMaterial = true;
            
            _copper.materialName = "CopperOre";
            _copper.outputMaterialType = OutputMaterialType.MetalBar;
            _copper.rawMaterialType = RawMaterialType.MetalOre;
            _copper.isRawMaterial = true;
        }

        [Test] // CRT-01
        public void Convert_ItemRecipe_ReturnsItemRecipeWithIngredients()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Bow";
            recipeSO.isItemRecipe = true;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = new MaterialSO { materialName = "Flax" }, quantity = 1 }
            };

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            Assert.IsInstanceOf<ItemRecipe>(recipe);
            Assert.AreEqual(1, recipe.Ingredients["Flax"]);
        }

        [Test] // CRT-02
        public void Convert_MaterialRecipe_ReturnsMaterialRecipeWithIngredients()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Bowstring";
            recipeSO.isItemRecipe = false;
            recipeSO.outputMaterialType = OutputMaterialType.Bowstring;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = new MaterialSO { materialName = "Flax" }, quantity = 1 }
            };

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            Assert.IsInstanceOf<MaterialRecipe>(recipe);
            Assert.AreEqual(1, recipe.Ingredients["Flax"]);
            var matRecipe = recipe as MaterialRecipe;
            Assert.AreEqual(OutputMaterialType.Bowstring, matRecipe?.MaterialType);
        }

        [Test] // CRT-03
        public void Convert_MissingMaterial_LogsWarningAndSkips()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Invalid";
            recipeSO.isItemRecipe = true;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = new MaterialSO { materialName = "Unknown" }, quantity = 1 }
            };

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            Assert.AreEqual(0, recipe.Ingredients.Count);
        }

        [Test] // CRT-04
        public void Convert_IngredientWithEmptyName_Skips()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "EmptyName";
            recipeSO.isItemRecipe = true;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = new MaterialSO { materialName = "" }, quantity = 1 }
            };

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            Assert.AreEqual(0, recipe.Ingredients.Count);
        }

        [Test] // CRT-05
        public void Convert_DuplicateIngredients_Throws()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Duplicate";
            recipeSO.isItemRecipe = true;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = new MaterialSO { materialName = "Flax" }, quantity = 1 },
                new MaterialQuantity { material = new MaterialSO { materialName = "Flax" }, quantity = 2 }
            };

            Assert.Throws<System.ArgumentException>(() => CraftingStationBehaviour.ConvertToRecipe(recipeSO));
        }

        [Test] // CRT-06
        public void Convert_NoIngredients_ReturnsEmptyDictionary()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Empty";
            recipeSO.isItemRecipe = true;
            recipeSO.ingredients = new List<MaterialQuantity>();

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            Assert.AreEqual(0, recipe.Ingredients.Count);
        }

        [Test] // CRT-07
        public void Convert_NullRecipeSO_Throws()
        {
            Assert.Throws<System.ArgumentNullException>(() => CraftingStationBehaviour.ConvertToRecipe(null));
        }

        [Test] // CRT-08
        public void Convert_PreservesAllowedStations()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Bronze Bar";
            recipeSO.isItemRecipe = false;
            recipeSO.allowedStations = new List<CraftingStationType>
            {
                CraftingStationType.Furnace
            };
            recipeSO.outputQuantity = 1;
            recipeSO.outputMaterialType = OutputMaterialType.MetalBar;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = _tin, quantity = 1 },
                new MaterialQuantity { material = _copper, quantity = 1 }
            };

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            CollectionAssert.AreEquivalent(recipeSO.allowedStations, recipe.AllowedCraftingStations);
        }

        [Test] // CRT-09
        public void Convert_OutputQuantity_Preserved()
        {
            var recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
            recipeSO.recipeName = "Bronze Bar";
            recipeSO.isItemRecipe = false;
            recipeSO.allowedStations = new List<CraftingStationType>
            {
                CraftingStationType.Furnace
            };
            recipeSO.outputQuantity = 1;
            recipeSO.outputMaterialType = OutputMaterialType.MetalBar;
            recipeSO.ingredients = new List<MaterialQuantity>
            {
                new MaterialQuantity { material = _tin, quantity = 1 },
                new MaterialQuantity { material = _copper, quantity = 1 }
            };

            var recipe = CraftingStationBehaviour.ConvertToRecipe(recipeSO);

            Assert.AreEqual(1, recipe.DefaultOutputQuantity);
        }
    }
}