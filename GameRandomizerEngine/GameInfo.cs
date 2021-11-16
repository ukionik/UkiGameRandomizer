namespace GameRandomizerEngine
{
    public class GameInfo
    {
        public string Name { get; }
        public string AltColor { get; }

        public GameInfo(string name, string altColor = null)
        {
            Name = name;
            AltColor = altColor;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}