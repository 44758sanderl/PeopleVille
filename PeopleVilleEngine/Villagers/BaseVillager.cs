using PeopleVilleEngine;
using PeopleVilleEngine.Items;
using PeopleVilleEngine.Items.Equiment;
using PeopleVilleEngine.Items.Cosumable.Food;
using PeopleVilleEngine.Locations;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }
    public List<BaseItem> Items { get; protected set; } = new();
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;

    readonly List<BaseItem> availableItems = new List<BaseItem>
    {
        new Apple(),
        new Water(),
        new TShirt(),
        new Bread(),
    };

    protected BaseVillager(Village village)
    {
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

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Age} years) Items: {string.Join(",", Items)}";
    }
}