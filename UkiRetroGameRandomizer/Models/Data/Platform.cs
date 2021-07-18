namespace UkiRetroGameRandomizer.Models.Data
{
    public class Platform
    {
        public string Name { get; }
        public string Caption { get; }
        public string FileName { get; }

        public Platform(string name, string caption, string fileName)
        {
            Name = name;
            Caption = caption;
            FileName = fileName;
        }

        public override string ToString()
        {
            return Caption;
        }
    }
}