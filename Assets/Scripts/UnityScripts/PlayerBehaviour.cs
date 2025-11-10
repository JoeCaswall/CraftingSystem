using System.Collections.Generic;
using System.Linq;
using Core;
using Registries;
using UnityEngine;
using Gameplay;
using ScriptableObjects;
using UnityEditor.Rendering;

namespace UnityScripts
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Player Player {get; private set;}
        private RawMaterial _tin;
        private RawMaterial _copper;

        private void Awake()
        {
            var craftedItems = new Dictionary<CraftedItem, int>();
            var materials = new Dictionary<string, int>();
            var inventory = new Inventory(craftedItems, materials);
            Player = new Player("TestUser", inventory);
            _tin = Registries.MaterialRegistry.Get("TinOre");
            _copper = Registries.MaterialRegistry.Get("CopperOre");
            Debug.Log($"Registry loaded: Tin={_tin?.Name}, Copper={_copper?.Name}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(Player == null ? "Player is null" : $"Player is: {Player.Name}");
                // POC for demo - add tin and copper to player inventory manually
                // In future items will be collected via gameplay
                if (_tin != null) Player.Inventory.AddMaterial(_tin, 1);
                if (_copper != null) Player.Inventory.AddMaterial(_copper, 1);
                Debug.Log("Added Tin and Copper from MaterialRegistry.");
            }
            
            // TODO: Implement proper interface for inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                var items = Player.Inventory.GetItemNames().ToArray();
                var materials = Player.Inventory.GetMaterialNames().ToArray();
                Debug.Log($"Inventory Contains:");
                
                for (int i = 0; i < materials.Length; i++)
                    try
                    {
                        var key = materials[i];
                        var quantity = Player.Inventory.GetMaterialQuantity(key); 
                        Debug.Log($"{key}: {quantity}");
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"Exception in material loop at index {i}: {ex}");
                    }
            }
        }
    }
}