using UnityEngine;
using Gameplay;
using UnityScripts;
using UnityEngine.UI;

namespace UI
{
    public class RecipeButtonBehaviour : MonoBehaviour
    {
        private Recipe recipe;
        private CraftingStationBehaviour station;

        public void Setup(Recipe recipe, CraftingStationBehaviour station)
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