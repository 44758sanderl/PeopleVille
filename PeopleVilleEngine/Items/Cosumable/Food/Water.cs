using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Cosumable.Food
{
    public class Water : BaseItem, IConsumable
    {
        public int HungerRestored { get; }

        public Water() : base("Water", "Last christmas i gave you my heart")
        {
            HungerRestored = 2;
        }
    }
}
