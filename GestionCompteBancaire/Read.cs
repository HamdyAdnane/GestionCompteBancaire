namespace ReadValeur
{
    public static class Read
    {
        public static int ReadIntBetween(string message, int minInclusive, int maxExclusive)
        {
            int result;
            bool verification = false;
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if ((int.TryParse(input, out result) && result >= minInclusive && result <= maxExclusive) == false)
                {
                    Console.WriteLine($"Saisie erronnée ,veuillez saisir un nombre entre {minInclusive} et {maxExclusive}");
                    verification = true;
                }
                else
                    verification = false;
            } while (verification == true);
            return result;
        }
        public static string ReadString(string message)
        {
            Console.Write(message);
            string result = Console.ReadLine();
            return result;
        }
        public static int ReadInt(string message)
        {
            int result;
            bool verification = false;
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result) == false)
                {
                    Console.WriteLine($"Saisie erronnée ,veuillez saisir un nombre.");
                    verification = true;
                }
                else
                    verification = false;
            } while (verification == true);
            return result;
        }
    }
}