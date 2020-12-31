﻿using System;
using System.Collections.Generic;
using Weighted_Randomizer;

namespace RandomItemGenerator
{
    class Weapon
    {
        
        public static string Randomize(string[] args)
        {
            //A simple randomizer method, pass it an array of strings and it will return a random one
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

        public static int CreateDamageStat(string weaponType, float rarityMultiplier)
        {
            //A method for creating randomly generated damage values based on weaponTypes and their hardcoded base damage values
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

        public static float CreateRarityMultiplier(string weaponRarity)
        {
            //Create a modifier float that can be used to enhance stats based on rarity (Like damage)
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
                case "Epic":
                    rarityMultiplier = 2.8f;
                    break;
                case "Legendary":
                    rarityMultiplier = 3.3f;
                    break;
            }

            return rarityMultiplier;
        }

        public static void Generator()
        {
            //The generator method which holds the prefix arrays, weapon type array and calls the randomizer functions and weighted randomizer library to build the name, rarity and stats of the item

            IWeightedRandomizer<string> rarityRandomizer = new DynamicWeightedRandomizer<string>();
            rarityRandomizer.Add("Common", 10);
            rarityRandomizer.Add("Magic", 6);
            rarityRandomizer.Add("Rare", 4);
            rarityRandomizer.Add("Epic", 2);
            rarityRandomizer.Add("Legendary", 1);

            string[] meleePrefixesList = { "Bronze", "Silver", "Golden" };
            string[] rangedPrefixesList = { "Accurate", "Fast", "Homing" };
            //TODO: Add functionality to prefixes: Elemental, speed, damage, crit?
            string[] weaponTypeList = { "Wand", "Sword", "Axe", "Bow" };
            string weaponType = Randomize(weaponTypeList);
            string weaponRarity = rarityRandomizer.NextWithReplacement();
            string prefix = RandomizePrefix(meleePrefixesList, rangedPrefixesList, weaponType);
            float rarityMultiplier = CreateRarityMultiplier(weaponRarity);
            int minBaseDamage = CreateDamageStat(weaponType, rarityMultiplier);
            int maxBaseDamage = minBaseDamage + new Random().Next(5,7);
            //float attacksPerSecond TODO: add attacks per second and DPS
            //int criticalChance; TODO: add crits
            //float criticalMultiplier; TODO: add crits

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
                case "Epic":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "Legendary":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine($"{weaponRarity + " " + weaponType}");

            //Now reset the console foreground colour
            Console.ResetColor();

            Console.WriteLine($"Damage: {minBaseDamage} - {maxBaseDamage}");
            Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key;
            Console.WriteLine("Random Item Generator by Jack Gibson");
            Console.WriteLine("Press ENTER to generate an item, press ESCAPE to quit!");

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
