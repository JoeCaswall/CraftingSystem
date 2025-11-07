using System.Collections.Generic;

namespace Gameplay
{
    public class RecipeBook
    {
        // Dictionary of recipe name to whether or not it has been learned
        private Dictionary<string, bool> _recipes;
        public RecipeBook(Dictionary<string, bool> recipes) {
            _recipes = recipes;
        }

        public void LearnRecipe(Recipe recipe)
        {
            _recipes[recipe.Name] = true;
        }
    }
}
