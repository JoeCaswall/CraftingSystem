using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Gameplay;

namespace Registries
{
    public static class MaterialRegistry
    {
        private static readonly Dictionary<string, RawMaterial> _byId = new Dictionary<string, RawMaterial>();

        public static void Register(RawMaterial material)
        {
            if (material == null || string.IsNullOrEmpty(material.Name)) return;
            if (!_byId.ContainsKey(material.Name))
                _byId[material.Name] = material;
        }

        public static RawMaterial Get(string name)
        {
            _byId.TryGetValue(name, out var material);
            return material;
        }

        public static IEnumerable<RawMaterial> GetAll() => _byId.Values;
    }
}