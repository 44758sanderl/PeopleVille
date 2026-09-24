using System.Linq;
using PeopleVilleEngine.Locations;

namespace PeopleVilleEngine.Villagers.Creators;

public class VillagerCreatorAdult : IVillagerCreator
{
    public bool CreateVillager(Village village)
    {
        var random = RNG.GetInstance();

        var adult = new AdultVillager(
            village,
            random.Next(18, 40)
        );

        // Find house
        var home = FindHome(village);

        if (home == null)
        {
            return false;
        }

        if (home.Villagers()
            .Count(v => v.GetType() == typeof(AdultVillager)) >= 1)
        {
            var first = home.Villagers()
                .First(v => v.GetType() == typeof(AdultVillager));

            adult.LastName = first.LastName;
            adult.IsMale = !first.IsMale;
            adult.FirstName = village.VillagerNameLibrary
                .GetRandomFirstName(adult.IsMale);
        }

        home.Villagers().Add(adult);
        adult.Home = home;

        // Add to village
        village.Villagers.Add(adult);

        return true;
    }

    private IHouse? FindHome(Village village)
    {
        var random = RNG.GetInstance();

        var potentialHomes = village.Locations
            .Where(p => p is IHouse)
            .Where(p => p.Villagers()
                .Count(v => v.GetType() == typeof(AdultVillager)) < 2)
            .Where(p =>
            {
                var house = (IHouse)p;

                return house.Population < house.MaxPopulation;
            })
            .Cast<IHouse>()
            .ToList();

        if (potentialHomes.Count > 0)
        {
            return potentialHomes[
                random.Next(0, potentialHomes.Count)
            ];
        }

        // No available house
        return null;
    }
}
