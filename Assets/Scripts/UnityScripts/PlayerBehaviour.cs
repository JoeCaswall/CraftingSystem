using System.Collections.Generic;
using Core;
using UnityEngine;
using Gameplay;

namespace UnityScripts
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Player Player;

        void Awake()
        {
            var craftedItems = new Dictionary<CraftedItem, int>();
            var materials = new Dictionary<IMaterial, int>();
            var inventory = new Inventory(craftedItems, materials);
            Player = new Player("TestUser", inventory);
        }
    }
}