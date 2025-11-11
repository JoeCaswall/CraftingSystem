using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Gameplay;

namespace Registries
{
    public static class MaterialRegistry
    {
        private static readonly Dictionary<string, RawMaterial> Materials;
        
        static MaterialRegistry()
        {
            Materials = new Dictionary<string, RawMaterial>();
        }

        public static void Register(RawMaterial material)
        {
            if (material == null || string.IsNullOrEmpty(material.Name)) return;
            if (!Materials.ContainsKey(material.Name))
                Materials[material.Name] = material;
        }

        public static RawMaterial Get(string name)
        {
            Materials.TryGetValue(name, out var material);
            return material;
        }

        public static void Clear()
        {
            Materials.Clear();
        }
        public static IEnumerable<RawMaterial> GetAll() => Materials.Values;
    }
}