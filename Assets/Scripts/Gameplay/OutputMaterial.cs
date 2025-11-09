using Core;
using Enums;

namespace Gameplay
{
    public class OutputMaterial : IMaterial
    {
        public string Name {get; set;}
        public OutputMaterialType Type {get; set;}
        
        public OutputMaterial(string name, OutputMaterialType type)
        {
            Name = name;
            Type = type;
        }

        public string GetMaterialCategory()
        {
            return "Output";
        }
    }
}