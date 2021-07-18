namespace GameRandomizerEngine
{
    public class GameInfo
    {
        public string Name { get; }

        public GameInfo(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}