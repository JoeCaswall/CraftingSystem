using Core;
using Enums;

namespace Gameplay
{
    public class RawMaterial : IMaterial
    {
        public string Name {get; set;}

        public RawMaterial(string name)
        {
            Name = name;
        }

        public string GetMaterialCategory()
        {
            return "Raw";
        }
    }
}