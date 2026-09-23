using System;
using System.Collections.Generic;

namespace PeopleVilleEngine.Locations
{
    public class Market : ILocation
    {
        private readonly Village _village;

        public Market(Village village)
        {
            _village = village;
        }

        public string Name => "Market";

        public List<BaseVillager> Villagers()
        {
            return _village.Villagers;
        }

        public void OpenMarket()
        {
            Console.WriteLine("Welcome to the Market!");

            var villagers = Villagers();

            for (int i = 0; i < villagers.Count; i++)
            {
                Console.WriteLine($"{i}: {villagers[i].FirstName}");
            }

            Console.WriteLine("Choose first villager index:");

            if (!int.TryParse(Console.ReadLine(), out int v1Index) ||
                v1Index < 0 ||
                v1Index >= villagers.Count)
            {
                Console.WriteLine("Invalid villager index.");
                return;
            }

            Console.WriteLine("Choose second villager index:");

            if (!int.TryParse(Console.ReadLine(), out int v2Index) ||
                v2Index < 0 ||
                v2Index >= villagers.Count)
            {
                Console.WriteLine("Invalid villager index.");
                return;
            }

            Trade(villagers[v1Index], villagers[v2Index]);
        }

        private void Trade(BaseVillager villager1, BaseVillager villager2)
        {
            Console.WriteLine(
                $"Trading between {villager1.FirstName} and {villager2.FirstName}"
            );

            Console.WriteLine(
                $"{villager1.FirstName}'s items: {string.Join(", ", villager1.Items)}"
            );

            Console.WriteLine(
                $"{villager2.FirstName}'s items: {string.Join(", ", villager2.Items)}"
            );

            if (villager1.Items.Count > 0 && villager2.Items.Count > 0)
            {
                var tempItem = villager1.Items[0];

                villager1.Items[0] = villager2.Items[0];
                villager2.Items[0] = tempItem;

                Console.WriteLine("Trade completed!");

                Console.WriteLine(
                    $"{villager1.FirstName}'s new items: {string.Join(", ", villager1.Items)}"
                );

                Console.WriteLine(
                    $"{villager2.FirstName}'s new items: {string.Join(", ", villager2.Items)}"
                );
            }
            else
            {
                Console.WriteLine("One of the villagers has no items to trade.");
            }
        }
    }
}
