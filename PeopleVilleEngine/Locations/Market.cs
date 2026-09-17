using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Locations
{
    public class Market
    {
        private readonly Village _village;

        public Market(Village village)
        {
            _village = village;
        }

        public void OpenMarket()
        {
            Console.WriteLine("Welcome to the Market!");
            var villagers = _village.Villagers;

            for (int i = 0; i < villagers.Count; i++)
                Console.WriteLine($"{i}: {villagers[i].FirstName}");

            Console.WriteLine("Choose first villager index:");
            int v1Index = int.Parse(Console.ReadLine());
            Console.WriteLine("Choose second villager index:");
            int v2Index = int.Parse(Console.ReadLine());

            Trade(villagers[v1Index], villagers[v2Index]);
        }

        private void Trade(BaseVillager villager1, BaseVillager villager2)
        {
            Console.WriteLine($"Trading between {villager1.FirstName} and {villager2.FirstName}");
            Console.WriteLine($"{villager1.FirstName}'s items: {string.Join(", ", villager1.Items)}");
            Console.WriteLine($"{villager2.FirstName}'s items: {string.Join(", ", villager2.Items)}");
            // For simplicity, let's just swap the first item of each villager
            if (villager1.Items.Count > 0 && villager2.Items.Count > 0)
            {
                var tempItem = villager1.Items[0];
                villager1.Items[0] = villager2.Items[0];
                villager2.Items[0] = tempItem;
                Console.WriteLine("Trade completed!");
                Console.WriteLine($"{villager1.FirstName}'s new items: {string.Join(", ", villager1.Items)}");
                Console.WriteLine($"{villager2.FirstName}'s new items: {string.Join(", ", villager2.Items)}");
            }
            else
            {
                Console.WriteLine("One of the villagers has no items to trade.");
            }
        }
    }
}
