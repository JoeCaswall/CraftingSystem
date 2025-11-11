using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gameplay
{
    public class Inventory
    {
        private Dictionary<CraftedItem, int> CraftedItems { get; }
        // Materials keyed by a stable string (e.g., material.Name or material.Id)
        public readonly Dictionary<string, int> Materials;
        public Inventory(Dictionary<CraftedItem, int> craftedItems, Dictionary<string, int> materials)
        {
            CraftedItems = craftedItems ?? new Dictionary<CraftedItem, int>();
            Materials = materials ?? new Dictionary<string, int>();
        }

        private IEnumerable<CraftedItem> GetItems()
        {
            return CraftedItems?.Keys ?? Enumerable.Empty<CraftedItem>();
        }

        private IEnumerable<string> GetMaterialKeys()
        {
            return Materials?.Keys ?? Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetItemNames()
        {
            return GetItems()
                .Select(i => i == null ? "<null>" : (i.Name ?? i.ToString()));
        }

        public IEnumerable<string> GetMaterialNames()
        {
            return GetMaterialKeys()
                .Select(k => string.IsNullOrEmpty(k) ? "<null>" : k);
        }

        public void AddItem(CraftedItem item, int quantity)
        {
            if (item == null || quantity <= 0) return;

            if (CraftedItems.TryGetValue(item, out var cur))
                CraftedItems[item] = cur + quantity;
            else
                CraftedItems[item] = quantity;
        }

        // Central string-based adder used by overloads
        private bool AddMaterialByKey(string materialKey, int quantity)
        {
            if (string.IsNullOrEmpty(materialKey) || quantity <= 0) return false;

            if (Materials.TryGetValue(materialKey, out var current))
            {
                Materials[materialKey] = current + quantity;
                return true;
            }
            Materials[materialKey] = quantity;
            return false;
        }

        private bool RemoveMaterialByKey(string materialKey, int quantity)
        {
            if (string.IsNullOrEmpty(materialKey) || quantity <= 0) return false;

            if (Materials.TryGetValue(materialKey, out var current))
            {
                if (quantity > current)
                {
                     return false;
                }
                Materials[materialKey] = current - quantity;
                return true;
            }

            return false;
        }
        
        public bool AddMaterial(OutputMaterial material, int quantity)
        {
            var key = material?.Name;
            Debug.Log($"Adding material: {key ?? "<null>"} qty:{quantity}");
            bool result = AddMaterialByKey(key, quantity);

            foreach (var kvp in Materials)
                Debug.Log($"Material in inventory: {kvp.Key} x{kvp.Value}");
            return result;
        }

        public bool AddMaterial(RawMaterial material, int quantity)
        {
            var key = material?.Name;
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("material name missing");
            }
            Debug.Log($"Adding material: {key ?? "<null>"} qty:{quantity}");
            bool result = AddMaterialByKey(key, quantity);

            foreach (var kvp in Materials)
                Debug.Log($"Material in inventory: {kvp.Key} x{kvp.Value}");
            return result;
        }
        
        public bool AddMaterial(IMaterial material, int quantity)
        {
            var key = material?.Name;
            Debug.Log($"Adding material: {key ?? "<null>"} quantity:{quantity}");
            bool result = AddMaterialByKey(key, quantity);

            foreach (var kvp in Materials)
                Debug.Log($"Material in inventory: {kvp.Key} x{kvp.Value}");
            return result;
        }

        public bool RemoveMaterial(IMaterial material, int quantity)
        {
            var key = material?.Name;
            if (material?.Name == null)
            {
                throw new ArgumentException("material name missing");
            }
            Debug.Log($"Removing material {key ?? "<null>"} quantity:{quantity}");
            bool result = RemoveMaterialByKey(key, quantity);
            return result;
        }
        
        public int GetMaterialQuantity(string materialKey)
        {
            if (string.IsNullOrEmpty(materialKey)) return 0;
            return Materials.TryGetValue(materialKey, out var qty) ? qty : 0;
        }
    }
}
