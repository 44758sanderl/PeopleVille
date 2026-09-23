using PeopleVilleEngine;
using PeopleVilleEngine.Items;
using PeopleVilleEngine.Items.Equiment;
using PeopleVilleEngine.Items.Cosumable.Food;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.lib.Exceptions;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public int Hunger { get; protected set; }
    public int MinHunger { get; protected set; }
    public int MaxHunger { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }
    public List<BaseItem> Items { get; protected set; } = new();
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;

    public ILocation CurrentLocation { get; set; }

    public event Action<BaseVillager>? Died;
    readonly List<BaseItem> availableItems = new List<BaseItem>
    {
        new Apple(),
        new Water(),
        new TShirt(),
        new Bread(),
    };

    public void MoveVillager(ILocation newLocation)
    {
        CurrentLocation.Villagers().Remove(this);
        newLocation.Villagers().Add(this);
        CurrentLocation = newLocation;
    }

    protected BaseVillager(Village village)
    {
        Hunger = 50;
        MaxHunger = 100;
        MinHunger = 0;
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        for (int i = 0; i < 3; i++)
        {
            int index = RNG.GetInstance().Next(availableItems.Count);
            Items.Add(availableItems[index]);
            availableItems.RemoveAt(index);
        }
    }

    public void Die()
    {
        Died?.Invoke(this);
    }

    public void Consume(IConsumable consumeable)
    {
        if (Hunger == MaxHunger)
        {
            throw new HungerAlreadyFullException();
        }

        Hunger = Math.Min(MaxHunger, Hunger + consumeable.HungerRestored);
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Age} years) Items: {string.Join(",", Items)}";
    }
}