using UnityEngine;
using Enums;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMaterial", menuName = "Crafting/Material")]
    public class MaterialSO : ScriptableObject
    {
        public string materialName;
        public RawMaterialType rawMaterialType;
        public OutputMaterialType outputMaterialType;
        public bool isRawMaterial;
    }
}