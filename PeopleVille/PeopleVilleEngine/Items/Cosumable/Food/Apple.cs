using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Cosumable.Food
{
    public class Apple : BaseItem, IConsumable
    {
        public int HungerRestored { get; }

        
        public Apple() : base("Apple", "Last christmas i gave you my heart")
        {
            HungerRestored = 10;
        }
        
    }
}
