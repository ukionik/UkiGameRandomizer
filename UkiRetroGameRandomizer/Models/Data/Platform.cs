using System;

namespace UkiRetroGameRandomizer.Models.Data
{
    public class Platform
    {
        public string Name { get; }
        public string Caption { get; }
        public string FileName { get; }

        public Platform(string metadata)
        {
            var arr = metadata.Split(';');
            Console.WriteLine(arr);

            Name = arr[0];
            Caption = arr[1];
            FileName = arr[2];
        }

        public override string ToString()
        {
            return Caption;
        }
    }
}