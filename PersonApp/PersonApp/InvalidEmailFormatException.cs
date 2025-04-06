using System;

namespace PersonApp.Exceptions
{
    public class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException()
            : base("Невірний формат електронної пошти.") { }
    }
}
