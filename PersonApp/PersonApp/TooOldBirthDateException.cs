using System;

namespace PersonApp.Exceptions
{
    public class TooOldBirthDateException : Exception
    {
        public TooOldBirthDateException()
            : base("Дата народження занадто стара. Можливо, ця особа вже не жива.") { }
    }
}
