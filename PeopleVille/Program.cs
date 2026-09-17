namespace PeopleVille;
using PeopleVilleEngine;
using PeopleVilleEngine.Items;
using PeopleVilleEngine.Locations;

internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PeopleVille");

            //Create village
            var village = new Village();
            Console.WriteLine(village.ToString());


        //Print locations with villagers to screen
        foreach (var location in village.Locations)
        {
            var locationStatus = location.Name;
            foreach (var villager in location.Villagers().OrderByDescending(v => v.Age))
            {
                locationStatus += $" {villager}";
                IConsumable? item = villager.Items.OfType<IConsumable>().FirstOrDefault();
                if (item != null)

                    {
                        try
                        {
                            villager.Consume(item);
                            Console.WriteLine($"Villager ate: {item} and do now have {villager.Hunger} hunger");
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                    } else {
                        Console.WriteLine("Villager don't have any consumeable items");
                    }
                     
                }
                Console.WriteLine(locationStatus);
            }
            while (true)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Go to the bank");
                Console.WriteLine("2. Go to the market");
                Console.WriteLine("3. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // Go to the bank
                        break;
                    case "2":
                        var market = new Market(village);
                        market.OpenMarket();
                        break;
                    case "3":
                        // Exit
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }

    }

