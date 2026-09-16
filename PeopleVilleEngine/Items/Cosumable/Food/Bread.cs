using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Cosumable.Food
{
    public class Bread : BaseItem
    {
        public int HungerRestored { get; }
        public Bread() : base("Bread")
        {
            HungerRestored = 25;
        }
    }
}
