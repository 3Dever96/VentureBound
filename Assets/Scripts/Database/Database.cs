using UnityEngine;

namespace VentureBound.Database
{
    public class Database : MonoBehaviour
    {
        public static Database instance;

        public RingStat[] stats;

        public Class[] classes;
        public Race[] races;
        public ElementAffinity[] elementAffinities;

        public Skill[] skills;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
