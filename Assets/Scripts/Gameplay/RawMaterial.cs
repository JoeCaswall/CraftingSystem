using Core;
using Enums;

namespace Gameplay
{
    public class RawMaterial : IMaterial
    {
        public string Name {get; set;}
        public float Value { get; set;}

        public RawMaterial(string name, float value)
        {
            Name = name;
            Value = value;
        }

        public string GetMaterialCategory()
        {
            return "Raw";
        }
    }
}