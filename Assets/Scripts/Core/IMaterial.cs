namespace Core
{
    public interface IMaterial : IItem
    {
        public string GetMaterialCategory(); // Returns "Raw" or "Output"
    }
}