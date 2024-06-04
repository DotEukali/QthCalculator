namespace DotEukali.QthCalculator
{
    public class MaidenHead
    {
        public MaidenHead()
        { }
        
        public MaidenHead(string location)
        {
            Location = location;
        }

        public string Location { get; set; }

        public int Length => Location.Length;

        public static MaidenHead Build(string location) => new MaidenHead(location);
        
    }
}
