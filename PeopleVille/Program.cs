namespace PeopleVille;
using PeopleVilleEngine;
using PeopleVilleEngine.Items;

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
        }
    }

