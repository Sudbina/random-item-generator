using System;
using System.Collections.Generic;
using Weighted_Randomizer;

namespace RandomItemGenerator
{
    class Weapon
    {
        
        public static string Randomize(string[] args)
        {
            //A simple randomizer method, pass it an array of strings and it will return a random one.
            Random rand = new Random();
            int idx = rand.Next(args.Length);
            string randomValue = args[idx];
            return randomValue;
        }

        public static string RandomizePrefix(string[] meleePrefixes, string[] rangedPrefixes, string WeaponType)
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

        public static int createDamageStat(string weaponType, float rarityMultiplier)
        {
            //A method for creating randomly generated damage values based on weaponTypes and their hardcoded base damage values.
            Random rand = new Random();
            float damageValue = 0;
            int rangedBaseDamage = 7;
            int meleeBaseDamage = 12;

            switch (weaponType)
            {
                case "Wand":
                case "Bow":
                    damageValue = (rangedBaseDamage + rand.Next(4)) * rarityMultiplier;
                    break;
                case "Sword":
                case "Axe":
                    damageValue = (meleeBaseDamage + rand.Next(8)) * rarityMultiplier;
                    break;
            }
            // Cast the float damageValue to an int to avoid messy damage numbers
            int finalValue = (int)damageValue;
            return finalValue;
        }

        public static float createRarityMultiplier(string weaponRarity)
        {
            float rarityMultiplier = 0.0f;

            switch (weaponRarity)
            {
                case "Common":
                    rarityMultiplier = 1.0f;
                    break;
                case "Magic":
                    rarityMultiplier = 1.8f;
                    break;
                case "Rare":
                    rarityMultiplier = 2.5f;
                    break;
                case "Legendary":
                    rarityMultiplier = 3.3f;
                    break;
            }

            return rarityMultiplier;
        }

        public static void Generator()
        {
            //The generator method which holds the prefix arrays, weapon type array and calls the randomizer functions to build the name of the item.

            IWeightedRandomizer<string> rarityRandomizer = new DynamicWeightedRandomizer<string>();
                rarityRandomizer.Add("Common", 4);
                rarityRandomizer.Add("Magic", 3);
                rarityRandomizer.Add("Rare", 2);
                rarityRandomizer.Add("Legendary", 1);

            string[] meleePrefixesList = { "Bronze", "Silver", "Golden" };
            string[] rangedPrefixesList = { "Accurate", "Fast", "Homing" };
            string[] weaponTypeList = { "Wand", "Sword", "Axe", "Bow" };
            string weaponType = Randomize(weaponTypeList);
            string weaponRarity = rarityRandomizer.NextWithReplacement();
            string prefix = RandomizePrefix(meleePrefixesList, rangedPrefixesList, weaponType);
            float rarityMultiplier = createRarityMultiplier(weaponRarity);
            int calculatedDamage = createDamageStat(weaponType, rarityMultiplier);
            //float attacksPerSecond;
            
            Console.WriteLine($"{prefix + " " + weaponType}");

            //Set this console line to match item rarity for some pizazz!
            switch (weaponRarity)
            {
                case "Magic":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "Rare":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "Legendary":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine($"{weaponRarity + " " + weaponType}");

            //Now reset the console foreground colour
            Console.ResetColor();

            Console.WriteLine($"Damage: {calculatedDamage}");
            Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key;
            Console.WriteLine("Random Item Generator by Jack Gibson");

            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter)
                {
                    Weapon.Generator();
                }
            } while (key != ConsoleKey.Escape);
        }
    }
}
