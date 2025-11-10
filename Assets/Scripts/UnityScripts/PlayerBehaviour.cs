using System.Collections.Generic;
using System.Linq;
using Core;
using Registries;
using UnityEngine;
using Gameplay;
using UnityEditor.Rendering;

namespace UnityScripts
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Player Player {get; private set;}

        private void Awake()
        {
            var craftedItems = new Dictionary<CraftedItem, int>();
            var materials = new Dictionary<IMaterial, int>();
            var inventory = new Inventory(craftedItems, materials);
            Player = new Player("TestUser", inventory);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(Player == null ? "Player is null" : $"Player is: {Player._name}");
                // POC for demo - add tin and copper to player inventory manually
                // In future items will be collected via gameplay
                foreach (var mat in Registries.MaterialRegistry.GetAll())
                {
                    if (mat == null) continue;
                    
                    if (mat.Name.Equals("TinOre") || mat.Name.Equals("CopperOre"))
                    {
                        Player.Inventory.AddMaterial(mat, 1);
                        Debug.Log($"Added {mat.Name} to inventory");
                    }
                }
            }
            
            // TODO: Implement proper interface for inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                var items = Player.Inventory.GetItemNames().ToArray();
                var materials = Player.Inventory.GetMaterialNames().ToArray();
                Debug.Log($"Inventory Contains: Items: {string.Join(", ", items)}, Materials: {string.Join(", ", materials)}");
            }
        }
    }
}