using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Cosumable.Food
{
    public class Water : BaseItem
    {
        public int HungerRestored { get; }
        public Water() : base("Water")
        {
            HungerRestored = 10;
        }
    }
}
