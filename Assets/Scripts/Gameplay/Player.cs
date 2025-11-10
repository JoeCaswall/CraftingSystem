namespace Gameplay
{
    public class Player
    {
        public string _name { get; private set; }
        public Inventory Inventory;
        public RecipeBook RecipeBook;

        public Player(string name, Inventory inventory)
        {
            _name = name;
            Inventory = inventory;
        }
    }
}