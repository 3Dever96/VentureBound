using UnityEngine;

namespace VentureBound.Database
{
    [CreateAssetMenu(fileName = "New Stat", menuName = "Database/Stat")]
    public class RingStat : ScriptableObject
    {
        public string statName;
        public string statAbb;
        public int statRingIndex;
        public float[] statValues;

        public float GetStatValue(int ringIndex)
        {
            int diff = Mathf.Abs(statRingIndex - ringIndex);

            int trueIndex = Mathf.Min(12 - diff, diff);

            return statValues[trueIndex];
        }
    }
}
