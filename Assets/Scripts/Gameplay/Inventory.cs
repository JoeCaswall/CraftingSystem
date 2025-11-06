using System.Collections.Generic;
using Core;

namespace Gameplay
{
    public class Inventory
    {
        private Player _belongsTo;
        public Dictionary<CraftedItem, int> CraftedItems;
        public Dictionary<IMaterial, int> Materials { get; }

        public Inventory(Player belongsTo, Dictionary<CraftedItem, int> craftedItems, Dictionary<IMaterial, int> materials)
        {   
            _belongsTo = belongsTo;
            Materials = materials;
            CraftedItems = craftedItems;
        }

        public void AddItem(CraftedItem item, int quantity)
        {
            if (CraftedItems.ContainsKey(item))
            {
                CraftedItems[item] += quantity;
            }
            CraftedItems.TryAdd(item, quantity);
        }
        
        public void AddMaterial(OutputMaterial material, int quantity)
        {
            if (Materials.ContainsKey(material))
            {
                Materials[material] += quantity;
            }
            Materials.TryAdd(material, quantity);
        }
    }
}