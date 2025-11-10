namespace Gameplay
{
    public class Player
    {
        public string Name { get; private set; }
        public Inventory Inventory;
        public RecipeBook RecipeBook;

        public Player(string name, Inventory inventory)
        {
            Name = name;
            Inventory = inventory;
        }
    }
}