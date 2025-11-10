using System.Collections.Generic;
using Enums;
using ScriptableObjects; // assumed namespace for MaterialSO
using Registries;
using UnityEngine;
using Gameplay;

namespace Gameplay
{
    public class MaterialLoader : MonoBehaviour
    {
        [SerializeField] private List<MaterialSO> materialAssets;

        void Awake()
        {
            if (materialAssets == null) return;

            foreach (var matSO in materialAssets)
            {
                var runtime = ConvertToMaterial(matSO);
                if (runtime != null)
                    MaterialRegistry.Register(runtime);
            }
        }

        // Converts from ScriptableObject asset to runtime RawMaterial
        private RawMaterial ConvertToMaterial(MaterialSO matSO)
        {
            if (matSO == null) return null;
            
            return new RawMaterial(matSO.name)
            {
                Name = matSO.name
            };
        }
    }
}