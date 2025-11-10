using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Gameplay
{
    public class Inventory
    {
        public Dictionary<CraftedItem, int> CraftedItems {get; private set;}
        public Dictionary<IMaterial, int> Materials { get; }

        
        public Inventory(Dictionary<CraftedItem, int> craftedItems, Dictionary<IMaterial, int> materials)
        {   
            Materials = materials;
            CraftedItems = craftedItems;
        }
        
        private IEnumerable<CraftedItem> GetItems()
        {
            return CraftedItems?.Keys ?? Enumerable.Empty<CraftedItem>();
        }

        private IEnumerable<IMaterial> GetMaterials()
        {
            return Materials?.Keys ?? Enumerable.Empty<IMaterial>();
        }
        
        public IEnumerable<string> GetItemNames()
        {
            return GetItems()
                .Select(i => i == null ? "<null>" : (i.Name ?? i.ToString()));
        }
        
        public IEnumerable<string> GetMaterialNames()
        {
            return GetMaterials()
                .Select(i => i == null ? "<null>" : (i.Name ?? i.ToString()));
        }

        public void AddItem(CraftedItem item, int quantity)
        {
            if (CraftedItems.ContainsKey(item))
            {
                CraftedItems[item] += quantity;
            }
            CraftedItems.Add(item, quantity);
        }
        
        public void AddMaterial(OutputMaterial material, int quantity)
        {
            Debug.Log($"Adding material: {material?.Name ?? "<null>"}");
            if (Materials.ContainsKey(material))
            {
                Materials[material] += quantity;
            }
            else
            {
                Materials.Add(material, quantity);
            }
            foreach (var kvp in Materials)
            {
                Debug.Log($"Material in inventory: {kvp.Key?.Name} x{kvp.Value}");
            }
        }
        
        public void AddMaterial(RawMaterial material, int quantity)
        {
            Debug.Log($"Adding material: {material?.Name ?? "<null>"}");
            if (Materials.ContainsKey(material))
            {
                Materials[material] += quantity;
            }
            else
            {
                Materials.Add(material, quantity);
            }
            foreach (var kvp in Materials)
            {
                Debug.Log($"Material in inventory: {kvp.Key?.Name} x{kvp.Value}");
            }
        }
    }
}