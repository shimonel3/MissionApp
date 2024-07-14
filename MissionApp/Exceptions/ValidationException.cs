using System;
namespace MissionApp.Exceptions
{
    [System.Serializable]
    public class ValidationException : System.Exception
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
    }
}