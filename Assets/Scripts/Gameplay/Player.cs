namespace Gameplay
{
    public class Player
    {
        private string _name;
        public Inventory Inventory;

        public Player(string name, Inventory inventory)
        {
            _name = name;
            Inventory = inventory;
        }
    }
}