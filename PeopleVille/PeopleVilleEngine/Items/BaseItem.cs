using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.Items
{
    public abstract class BaseItem
    {
        public string Name { get; }
        public string Description { get; }
        public int Value { get; private set; }

        protected BaseItem (string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void SetValue(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
 