using System;

namespace RandomItemGenerator
{
    class Weapon
    {
        public static string Randomize(String[] args)
        {
            //A simple randomizer method, pass it an array of strings and it will return a random one.
            Random rand = new Random();
            int idx = rand.Next(args.Length);
            string randomValue = args[idx];
            return randomValue;
        }

        public static string RandomizePrefix(String[] meleePrefixes, String[] rangedPrefixes, String WeaponType)
        {
            //A more complex randomizer method for rolling prefixes based on the weapon type, passed in the melee and ranged prefix arrays and the weapon type string
            Random rand = new Random();
            int idx;
            string chosenPrefix = "";

            switch (WeaponType)
            {
                case "Wand":
                case "Bow":
                    idx = rand.Next(rangedPrefixes.Length);
                    chosenPrefix = rangedPrefixes[idx];
                    break;
                case "Sword":
                case "Axe":
                    idx = rand.Next(meleePrefixes.Length);
                    chosenPrefix = meleePrefixes[idx];
                    break;
            }

            return chosenPrefix;
        }

        public static int createDamageStat(String weaponType)
        {
            //A method for creating randomly generated damage values based on weaponTypes and their hardcoded base damage values.
            Random rand = new Random();
            int damageValue = 0;
            int rangedBaseDamage = 7;
            int meleeBaseDamage = 12;

            switch (weaponType)
            {
                case "Wand":
                case "Bow":
                    damageValue = rangedBaseDamage + rand.Next(4);
                    break;
                case "Sword":
                case "Axe":
                    damageValue = meleeBaseDamage + rand.Next(8);
                    break;
            }

            return damageValue;
        }

        public static void Generator()
        {
            //The generator method which holds the prefix arrays, weapon type array and calls the randomizer functions to build the name of the item.
            string[] meleePrefixesList = { "Bronze", "Silver", "Golden" };
            string[] rangedPrefixesList = { "Accurate", "Fast", "Homing" };
            string[] weaponTypeList = { "Wand", "Sword", "Axe", "Bow" };
            string weaponType = Randomize(weaponTypeList);
            string prefix = RandomizePrefix(meleePrefixesList, rangedPrefixesList, weaponType);
            int calculatedDamage = createDamageStat(weaponType);

            Console.WriteLine($"Generated: {prefix + " " + weaponType}");
            Console.WriteLine($"Damage: {calculatedDamage}");
            Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Random Item Generator by Jack Gibson");
            Console.WriteLine("Press ENTER to generate and ESCAPE to exit!");
            while (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                Weapon.Generator();
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
