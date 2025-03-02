using System.Collections.Generic;

namespace SpeedJam
{
    public class ListsOfObjects
    {
        public List<GravitationalObject> GravitationalObjects { get; } = new();
        public List<Star> Stars { get; } = new();
    }
}