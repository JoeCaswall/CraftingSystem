using System.Collections.Generic;

namespace Gameplay
{
    public class RecipeBook
    {
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
