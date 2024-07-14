using System;
namespace MissionApp.Exceptions
{
    [System.Serializable]
    public class MissionException : System.Exception
    {
        public MissionException() { }
        public MissionException(string message) : base(message) { }
    }
}