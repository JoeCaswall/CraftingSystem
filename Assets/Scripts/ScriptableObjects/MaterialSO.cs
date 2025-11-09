using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMaterial", menuName = "Crafting/Material")]
    public class MaterialSO : ScriptableObject
    {
        public string materialName;
        public Sprite icon;
        public string description;
        public float value;
        public bool isRawMaterial;
    }
}