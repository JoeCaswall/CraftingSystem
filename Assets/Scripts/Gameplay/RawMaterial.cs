using Core;
using Registries;

namespace Gameplay
{
    public class RawMaterial : IMaterial
    {
        public string Name {get; set;}

        public RawMaterial(string name)
        {
            Name = name;
            // Ensure any created recipes get added to the registry
            MaterialRegistry.Register(this);
        }

        public string GetMaterialCategory()
        {
            return "Raw";
        }
    }
}