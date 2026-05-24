using UnityEngine;
using VentureBound.Database;

namespace VentureBound.CharacterData
{
    public class CharacterStats : MonoBehaviour
    {
        public Class myClass;
        public ElementAffinity myAffinity;

        public float maxHP;
        public float maxMP;
        public float maxSP;
        public float ATK;
        public float DEF;
        public float MATK;
        public float MDEF;
        public float AGI;
        public float LUK;
        public float WIS;
        public float CHA;
        public float INT;

        public float currentHP;
        public float currentMP;
        public float currentSP;

        protected virtual void SetStats()
        {

        }
    }
}
