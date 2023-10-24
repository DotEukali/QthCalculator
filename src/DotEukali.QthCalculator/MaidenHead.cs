namespace DotEukali.QthCalculator
{
    public class MaidenHead
    {
        public MaidenHead(string location)
        {
            Location = location;
        }

        public readonly string Location;

        public int Length => Location.Length;

        public static MaidenHead Build(string location) => new MaidenHead(location);
        
    }
}
