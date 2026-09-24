using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Cosumable.Food
{
    public class Bread : BaseItem, IConsumable
    {
        public int HungerRestored { get; }


        public Bread() : base("Bread", "Last christmas i gave you my heart")
        {
            HungerRestored = 25;
        }

    }
}
