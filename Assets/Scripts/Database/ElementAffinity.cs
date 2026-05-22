using UnityEngine;

namespace VentureBound
{
    [CreateAssetMenu(fileName = "New ElementAffinity", menuName = "Database/ElementAffinity")]
    public class ElementAffinity : ScriptableObject
    {
        public string affinityName;
        public int affinityRingIndex;
    }
}
