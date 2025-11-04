using System.Collections.Generic;

namespace Gameplay
{
    public class RecipeBook
    {
        private Dictionary<Recipe, bool> _recipes;
        public RecipeBook(Dictionary<Recipe, bool> recipes) {
            _recipes = recipes;
        }
    }
    
}