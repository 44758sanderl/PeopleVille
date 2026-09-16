using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Cosumable.Food
{
    public class Apple : BaseItem
    {
        public int HungerRestored { get; }
        public Apple() : base("Apple")
        {
            HungerRestored = 10;
        }
    }
}
