using Core;
using Enums;

namespace Gameplay
{
    public class OutputMaterial : IMaterial
    {
        public string Name {get; set;}
        public float Value {get; set;}
        public OutputMaterialType Type {get; set;}
        
        public OutputMaterial(string name, float value, OutputMaterialType type)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        public string GetMaterialCategory()
        {
            return "Output";
        }
    }
}