using UnityEngine;

namespace VentureBound
{
    [CreateAssetMenu(fileName = "New Race", menuName = "Database/Race")]
    public class Race : ScriptableObject
    {
        public string raceName;
        public int raceRingIndex;
    }
}
