using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVilleEngine.lib.Exceptions
{
    public class HungerAlreadyFullException : Exception
    {
        public HungerAlreadyFullException()
            : base("Karakteren er allerede mæt og kan ikke spise mere.") { }

        public HungerAlreadyFullException(string message)
            : base(message) { }

        public HungerAlreadyFullException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
