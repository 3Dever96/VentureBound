using UnityEngine;

namespace VentureBound.Database
{
    [CreateAssetMenu(fileName = "New Class", menuName = "Database/Class")]
    public class Class : ScriptableObject
    {
        public string className;
        public int classRingIndex;
    }
}
