using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items.Equiment
{
    public class TShirt : BaseItem
    {
        public int HungerRestored { get; }
        public TShirt() : base("TShirt")
        {
            HungerRestored = 10;
        }
    }
}
