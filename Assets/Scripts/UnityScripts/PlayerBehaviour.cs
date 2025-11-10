using System.Collections.Generic;
using Core;
using UnityEngine;
using Gameplay;
using UnityEditor.Rendering;

namespace UnityScripts
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Player Player {get; private set;}
        public OutputMaterial TinOre;
        public OutputMaterial CopperOre;

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
                if (TinOre != null)
                    Player.Inventory.AddMaterial(TinOre, 1);
                if (CopperOre != null)
                    Player.Inventory.AddMaterial(CopperOre, 1);
                Debug.Log("Added Tin and Copper to inventory.");
            }
            
            // TODO: Implement proper interface for inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log($"Inventory Contains: Items: {Player.Inventory.CraftedItems.Keys}, Materials: {Player.Inventory.Materials.Keys}");
            }
        }
    }
}