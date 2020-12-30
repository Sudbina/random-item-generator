using System;

namespace RandomItemGenerator
{
    class Weapon
    {
        public static string Randomize(String[] args)
        {
            Random rand = new Random();
            int idx = rand.Next(args.Length);
            string randomValue = args[idx];
            return randomValue;
        }
        public static void Generator()
        {
            string[] prefixesList = { "Bronze", "Silver", "Golden" };
            string[] weaponTypeList = { "Wand", "Sword", "Axe" };
            string prefix = Randomize(prefixesList);
            string weaponType = Randomize(weaponTypeList);

            Console.WriteLine($"Selected {prefix + " " + weaponType}");
            Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Weapon.Generator();
        }
    }
}
