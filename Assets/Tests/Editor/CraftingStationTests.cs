using NUnit.Framework;
using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.Xml;
using Core;
using Enums;
using Gameplay;
using Registries;
using WorldObjects;

namespace Tests.Editor
{
    [TestFixture]
    public class CraftingStationTests
    {
        private CraftingStation _furnace;
        private CraftingStation _spinningWheel;
        private Player _player;
        private MaterialRecipe _bronzeRecipe;
        private MaterialRecipe _bowStringRecipe;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            MaterialRegistry.Clear();
            MaterialRegistry.Register(new RawMaterial("Flax"));
            MaterialRegistry.Register(new RawMaterial("TinOre"));
            MaterialRegistry.Register(new RawMaterial("Copper"));
            MaterialRegistry.Register(new RawMaterial("BronzeBar"));
            
            _furnace = new CraftingStation("Furnace", CraftingStationType.Furnace);
            
            _spinningWheel = new CraftingStation("SpinningWheel", CraftingStationType.SpinningWheel);

            var bronzeIngredients = new Dictionary<string, int>
            {
                { "TinOre", 1 },
                { "Copper", 1 }
            };

            var bowStringIngredients = new Dictionary<string, int>
            {
                { "Flax", 1 }
            };

            _bronzeRecipe = new MaterialRecipe(
                "Bronze Bar",
                OutputMaterialType.MetalBar,
                "Metals",
                bronzeIngredients,
                new List<CraftingStationType> { CraftingStationType.Furnace },
                1
            );

            _bowStringRecipe = new MaterialRecipe(
                "Bowstring",
                OutputMaterialType.Bowstring,
                "Weapons",
                bowStringIngredients,
                new List<CraftingStationType> { CraftingStationType.SpinningWheel },
                5
            );
        }
        
        [SetUp]
        public void Setup()
        {
            // Clears _existingNames as a reflection so that Recipe's duplicate item check doesn't fail the tests
            // var field = typeof(Recipe).GetField("_existingNames",
            //     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            // var set = (HashSet<string>)field.GetValue(null);
            // set.Clear();
            MaterialRegistry.Clear();
            MaterialRegistry.Register(new RawMaterial("Flax"));
            MaterialRegistry.Register(new RawMaterial("TinOre"));
            MaterialRegistry.Register(new RawMaterial("Copper"));
            MaterialRegistry.Register(new RawMaterial("BronzeBar"));

            _player = new Player("TestUser", new Inventory(
                new Dictionary<CraftedItem, int>(),
                new Dictionary<string, int>()
                ));
        }

        [Test] // CS-01
        public void Craft_SucceedsWithCorrectStationAndIngredients()
        {
            _player.Inventory.AddMaterial(MaterialRegistry.Get("TinOre"), 1);
            _player.Inventory.AddMaterial(MaterialRegistry.Get("Copper"), 1);

            var output = _furnace.Craft(_bronzeRecipe, _player);

            Assert.NotNull(output);
            Assert.AreEqual(0, _player.Inventory.GetMaterialQuantity("TinOre"));
            Assert.AreEqual(0, _player.Inventory.GetMaterialQuantity("Copper"));
            Assert.AreEqual(1, _player.Inventory.GetMaterialQuantity("Bronze Bar"));
        }

        [Test] // CS-02
        public void Craft_WrongStationType_Throws()
        {
            var anvil = new CraftingStation("Anvil", CraftingStationType.Anvil);
            _player.Inventory.AddMaterial(MaterialRegistry.Get("TinOre"), 1);
            _player.Inventory.AddMaterial(MaterialRegistry.Get("Copper"), 1);

            Assert.Throws<InvalidOperationException>(() => anvil.Craft(_bronzeRecipe, _player));
        }

        [Test] // CS-03
        public void Craft_InsufficientIngredients_Throws()
        {
            _player.Inventory.AddMaterial(MaterialRegistry.Get("TinOre"), 1);

            Assert.Throws<InvalidOperationException>(() => _furnace.Craft(_bronzeRecipe, _player));
        }

        [Test] // CS-04
        public void Craft_ConsumesCorrectQuantities()
        {
            _player.Inventory.AddMaterial(MaterialRegistry.Get("TinOre"), 2);
            _player.Inventory.AddMaterial(MaterialRegistry.Get("Copper"), 2);

            _furnace.Craft(_bronzeRecipe, _player);

            Assert.AreEqual(1, _player.Inventory.GetMaterialQuantity("TinOre"));
            Assert.AreEqual(1, _player.Inventory.GetMaterialQuantity("Copper"));
        }

        [Test] // CS-05
        public void Craft_ProducesCorrectOutputQuantity()
        {
            var recipeWithFiveOutput = _bowStringRecipe;

            _player.Inventory.AddMaterial(MaterialRegistry.Get("Flax"), 5);

            _spinningWheel.Craft(recipeWithFiveOutput, _player);

            Assert.AreEqual(5, _player.Inventory.GetMaterialQuantity("Bowstring"));
        }

        [Test] // CS-06
        public void Craft_ReturnsCorrectOutputMaterial()
        {
            _player.Inventory.AddMaterial(MaterialRegistry.Get("TinOre"), 1);
            _player.Inventory.AddMaterial(MaterialRegistry.Get("Copper"), 1);

            var output = _furnace.Craft(_bronzeRecipe, _player);

            Assert.AreEqual(OutputMaterialType.MetalBar, output.Type);
        }

        [Test] // CS-07
        public void Craft_NullRecipe_Throws()
        {
            Assert.Throws<NullReferenceException>(() => _furnace.Craft(null, _player));
        }

        [Test] // CS-08
        public void Craft_NullPlayer_Throws()
        {
            Assert.Throws<NullReferenceException>(() => _furnace.Craft(_bronzeRecipe, null));
        }
    }
}