using Indexers;

namespace Indexers
{ 
    class ValuePrintWidth
    {
        public int DefaultCharWidth { get; private set; }
        public ValuePrintWidth(int defaultCharacterWidth)
        {
            DefaultCharWidth = defaultCharacterWidth;
        }

        // First indexer
        public int this[string s, int characterWidth]
        {
            get { return s.Length * characterWidth; }
        }

        // Second indexer
        public int this[double x, int characterWidth]
        {
            get                                           
            {
                string s = Convert.ToString(x);
                return this[s, characterWidth];
            }
        }


        // Third indexer
        public int this[string s]
        {
            get
            {
                return this[s, DefaultCharWidth];
            }
        }

        // Fourth indexer
        public int this[double x]
        {
            get
            {
                string s = Convert.ToString(x);
                return this[s];
            }
        }
    }
                                                                                                              16.649
    class Program
    {
        static void Main()
        {
            ValuePrintWidth x = new ValuePrintWidth(10);

            Console.WriteLine(x["Ziad"]);
            Console.WriteLine(x[16.649]);
            Console.WriteLine(x["Ziad", 20]);
            Console.WriteLine(x[16.649, 20]);
        }
    }
}