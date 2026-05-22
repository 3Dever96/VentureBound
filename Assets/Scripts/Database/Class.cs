using UnityEngine;

namespace VentureBound
{
    [CreateAssetMenu(fileName = "New Class", menuName = "Database/Class")]
    public class Class : ScriptableObject
    {
        public string className;
        public int classRingIndex;
    }
}
