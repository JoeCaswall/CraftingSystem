using Core;
using ScriptableObjects;

namespace UnityScripts
{
    // Wraps MaterialSO in a new class implementing IMaterial so that the Recipe class can consume it properly
    // This enables the use of the unity inspector to populate our materials and recipes
    public class MaterialWrapper : IMaterial
    {
        public MaterialSO Source { get; }
        public string Name => Source.materialName;
        
        public MaterialWrapper(MaterialSO source)
        {
            Source = source;
        }

        public string GetMaterialCategory()
        {
            return Source.isRawMaterial ? "raw" : "output";
        }
    }
}