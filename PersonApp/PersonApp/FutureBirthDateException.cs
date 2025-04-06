using System;

namespace PersonApp.Exceptions
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException()
            : base("Дата народження не може бути в майбутньому.") { }
    }
}
