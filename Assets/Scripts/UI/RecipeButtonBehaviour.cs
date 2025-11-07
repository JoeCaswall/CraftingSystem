using UnityEngine;
using Gameplay;
using UnityEngine.UI;

namespace UnityScripts
{
    public class RecipeButtonBehaviour : MonoBehaviour
    {
        private Recipe recipe;
        private CraftingStationbehaviour station;

        public void Setup(Recipe recipe, CraftingStationbehaviour station)
        {
            this.recipe = recipe;
            this.station = station;
            GetComponentInChildren<Text>().text = recipe.Name;
        }

        public void OnClick()
        {
            station.TryCraft(recipe);
        }
    }
}