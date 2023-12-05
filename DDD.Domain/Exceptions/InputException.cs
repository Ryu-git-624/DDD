using System;

namespace DDDTest.Tests
{
    public sealed class InputException : Exception

    {
        public InputException(string message) : base(message)
        {

        }
    }
}