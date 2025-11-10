using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

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
        private void AddMaterialByKey(string materialKey, int quantity)
        {
            if (string.IsNullOrEmpty(materialKey) || quantity <= 0) return;

            if (Materials.TryGetValue(materialKey, out var current))
                Materials[materialKey] = current + quantity;
            else
                Materials[materialKey] = quantity;
        }
        
        public void AddMaterial(OutputMaterial material, int quantity)
        {
            var key = material?.Name;
            Debug.Log($"Adding material: {key ?? "<null>"} qty:{quantity}");
            AddMaterialByKey(key, quantity);

            foreach (var kvp in Materials)
                Debug.Log($"Material in inventory: {kvp.Key} x{kvp.Value}");
        }

        public void AddMaterial(RawMaterial material, int quantity)
        {
            var key = material?.Name;
            Debug.Log($"Adding material: {key ?? "<null>"} qty:{quantity}");
            AddMaterialByKey(key, quantity);

            foreach (var kvp in Materials)
                Debug.Log($"Material in inventory: {kvp.Key} x{kvp.Value}");
        }
        
        public void AddMaterial(IMaterial mat, int quantity)
        {
            var key = mat?.Name;
            Debug.Log($"Adding material (IMaterial): {key ?? "<null>"} qty:{quantity}");
            AddMaterialByKey(key, quantity);

            foreach (var kvp in Materials)
                Debug.Log($"Material in inventory: {kvp.Key} x{kvp.Value}");
        }
        
        public int GetMaterialQuantity(string materialKey)
        {
            if (string.IsNullOrEmpty(materialKey)) return 0;
            return Materials.TryGetValue(materialKey, out var qty) ? qty : 0;
        }
    }
}
