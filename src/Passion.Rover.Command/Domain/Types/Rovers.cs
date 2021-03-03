using System.Collections.Generic;

namespace Passion.Rover.Command.Domain.Types
{
    public class Rovers
    {
        public const string Passion = "603e9272e16a84e9e5405895";
        public const string Curiosity = "853a9978y16a84e872a54058a1";

        public static IEnumerable<string> GetAllRoverTypes()
        {
            yield return Passion;
            yield return Curiosity;
        }
    }
}