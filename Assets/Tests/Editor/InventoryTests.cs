using NUnit.Framework;
using Gameplay;
using System.Collections.Generic;

namespace Tests.Editor
{
    [TestFixture]
    public class InventoryTests
    {
        private Inventory _inventory;
        private RawMaterial _flax;
        private RawMaterial _tin;

        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory(
                craftedItems: new Dictionary<CraftedItem, int>(),
                materials: new Dictionary<string, int>()
            );

            _flax = new RawMaterial(
                name: "Flax"
            );

            _tin = new RawMaterial(
                name: "Tin"
            );
        }

        [Test] // INV-01
        public void AddMaterial_Normal_AddsQuantity()
        {
            _inventory.AddMaterial(_flax, 5);
            Assert.AreEqual(5, _inventory.GetMaterialQuantity("Flax"));
        }

        [Test] // INV-02
        public void AddMaterial_Twice_SumsQuantity()
        {
            _inventory.AddMaterial(_flax, 5);
            _inventory.AddMaterial(_flax, 3);
            Assert.AreEqual(8, _inventory.GetMaterialQuantity("Flax"));
        }

        [Test] // INV-03
        public void RemoveMaterial_SufficientQuantity_Decrements()
        {
            _inventory.AddMaterial(_flax, 5);
            bool result = _inventory.RemoveMaterial(_flax, 3);
            Assert.IsTrue(result);
            Assert.AreEqual(2, _inventory.GetMaterialQuantity("Flax"));
        }

        [Test] // INV-04
        public void RemoveMaterial_InsufficientQuantity_Fails()
        {
            _inventory.AddMaterial(_flax, 2);
            bool result = _inventory.RemoveMaterial(_flax, 5);
            Assert.IsFalse(result);
            Assert.AreEqual(2, _inventory.GetMaterialQuantity("Flax"));
        }

        [Test] // INV-05
        public void GetMaterialQuantity_NonExistent_ReturnsZero()
        {
            Assert.AreEqual(0, _inventory.GetMaterialQuantity("Copper"));
        }

        [Test] // INV-06
        public void AddMaterial_ZeroQuantity_NoChange()
        {
            _inventory.AddMaterial(_tin, 0);
            Assert.AreEqual(0, _inventory.GetMaterialQuantity("Tin"));
        }

        [Test] // INV-07
        public void AddMaterial_Misformed_Key_Throws()
        {
            var invalidMaterial = new RawMaterial(name: string.Empty);
            Assert.Throws<System.ArgumentException>(() => _inventory.AddMaterial(invalidMaterial, 5));
        }
    }
}