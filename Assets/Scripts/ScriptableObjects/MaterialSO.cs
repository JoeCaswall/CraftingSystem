using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMaterial", menuName = "Crafting/Material")]
    public class MaterialSO : ScriptableObject
    {
        public string materialName;
        public bool isRawMaterial;
    }
}