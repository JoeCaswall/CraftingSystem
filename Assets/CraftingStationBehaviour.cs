using UnityEngine;
using WorldObjects;
using Gameplay;
using Enums;

public class FurnaceBehaviour : MonoBehaviour
{
    public string stationName;
    public CraftingStationType stationType;
    public Player Player;
    
    private CraftingStation _station;
    
    void Awake()
    {
        _station = new CraftingStation(stationName, stationType);
    }
    
    public void TryCraft(Recipe recipe)
    {
        if (recipe is ItemRecipe itemRecipe)
        {
            var result = _station.Craft(itemRecipe, Player);
            Debug.Log($"Crafted item: {result.Name}");
        }
        else if (recipe is MaterialRecipe materialRecipe)
        {
            var result = _station.Craft(materialRecipe, Player);
            Debug.Log($"Refined material: {result.Name}");
        }
        else
        {
            Debug.LogWarning("Unknown recipe type.");
        }
    }
}
