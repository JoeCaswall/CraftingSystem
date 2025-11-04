using System.Collections.Generic;
using Core;

namespace Gameplay
{
    public class Inventory
    {
        private Player _belongsTo;
        private List<CraftedItem> _craftedItems;
        private List<IMaterial> _materials;

        public Inventory(Player belongsTo, List<CraftedItem> craftedItems, List<IMaterial> materials)
        {   
            _materials = materials;
            _belongsTo = belongsTo;
            _craftedItems = craftedItems;
        }
        
        
    }
}