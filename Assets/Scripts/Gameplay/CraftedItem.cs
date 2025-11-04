using Core;
using Enums;

namespace Gameplay
{
    public class CraftedItem : IItem
    {
        public string Name {get; set;}
        public float Value {get; set;}
        public CraftedItemType Type { get; set; }

        public CraftedItem(string name, float value, CraftedItemType type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
}