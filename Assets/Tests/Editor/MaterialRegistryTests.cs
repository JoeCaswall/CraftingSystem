using System.Linq;
using NUnit.Framework;
using Registries;
using Core;
using Gameplay;

namespace Tests.Editor
{
    [TestFixture]
    public class MaterialRegistryTests
    {
        [SetUp]
        public void Setup()
        {
            MaterialRegistry.Clear();
        }

        [Test] // MR-01
        public void Register_ValidMaterial_StoresInRegistry()
        {
            var flax = new RawMaterial("Flax");
            MaterialRegistry.Register(flax);

            var result = MaterialRegistry.Get("Flax");
            Assert.NotNull(result);
            Assert.AreSame(flax, result);
        }

        [Test] // MR-02
        public void Register_NullMaterial_DoesNothing()
        {
            MaterialRegistry.Register(null);
            Assert.IsEmpty(MaterialRegistry.GetAll());
        }

        [Test] // MR-03
        public void Register_EmptyNameMaterial_DoesNothing()
        {
            var invalid = new RawMaterial("");
            MaterialRegistry.Register(invalid);
            Assert.IsEmpty(MaterialRegistry.GetAll());
        }

        [Test] // MR-04
        public void Register_DuplicateMaterial_OnlyOneStored()
        {
            var flax1 = new RawMaterial("Flax");
            var flax2 = new RawMaterial("Flax");

            MaterialRegistry.Register(flax1);
            MaterialRegistry.Register(flax2);

            var all = MaterialRegistry.GetAll();
            Assert.AreEqual(1, all.Count());
            Assert.AreSame(flax1, MaterialRegistry.Get("Flax"));
        }

        [Test] // MR-05
        public void Get_ExistingMaterial_ReturnsMaterial()
        {
            var copper = new RawMaterial("Copper");
            MaterialRegistry.Register(copper);

            var result = MaterialRegistry.Get("Copper");
            Assert.NotNull(result);
            Assert.AreSame(copper, result);
        }

        [Test] // MR-06
        public void Get_NonExistingMaterial_ReturnsNull()
        {
            var result = MaterialRegistry.Get("Unknown");
            Assert.IsNull(result);
        }

        [Test] // MR-07
        public void Clear_RemovesAllMaterials()
        {
            var tin = new RawMaterial("Tin");
            MaterialRegistry.Register(tin);

            MaterialRegistry.Clear();

            Assert.IsEmpty(MaterialRegistry.GetAll());
            Assert.IsNull(MaterialRegistry.Get("Tin"));
        }

        [Test] // MR-08
        public void GetAll_ReturnsAllRegisteredMaterials()
        {
            var tin = new RawMaterial("Tin");
            var copper = new RawMaterial("Copper");

            MaterialRegistry.Register(tin);
            MaterialRegistry.Register(copper);

            var all = MaterialRegistry.GetAll();
            CollectionAssert.AreEquivalent(new[] { tin, copper }, all);
        }
    }
}