using System.Collections.Generic;
using UnityEngine;
using VentureBound.Database;

namespace VentureBound.CharacterData
{
    public class PlayerStats : CharacterStats
    {
        public Race myRace;

        public Dictionary<string, Skill> skills = new Dictionary<string, Skill>();

        private void Start()
        {
            int classIndex = Random.Range(0, 12);
            int raceIndex = Random.Range(0, 12);
            int affinityIndex = Random.Range(0, 12);

            myClass = Database.Database.instance.classes[classIndex];
            myRace = Database.Database.instance.races[raceIndex];
            myAffinity = Database.Database.instance.elementAffinities[affinityIndex];

            SetStats();

            for (var i = 0; i < Database.Database.instance.skills.Length; i++)
            {
                Skill skill = Database.Database.instance.skills[i];

                skills.Add(skill.skillName, skill);
            }

            print(skills.Count);
        }

        protected override void SetStats()
        {
            Database.Database database = Database.Database.instance;

            DEF = database.stats[0].GetStatValue(myClass.classRingIndex) + database.stats[0].GetStatValue(myRace.raceRingIndex) + database.stats[0].GetStatValue(myAffinity.affinityRingIndex);
            WIS = database.stats[1].GetStatValue(myClass.classRingIndex) + database.stats[1].GetStatValue(myRace.raceRingIndex) + database.stats[1].GetStatValue(myAffinity.affinityRingIndex);
            MATK = database.stats[2].GetStatValue(myClass.classRingIndex) + database.stats[2].GetStatValue(myRace.raceRingIndex) + database.stats[2].GetStatValue(myAffinity.affinityRingIndex);
            maxMP = database.stats[3].GetStatValue(myClass.classRingIndex) + database.stats[3].GetStatValue(myRace.raceRingIndex) + database.stats[3].GetStatValue(myAffinity.affinityRingIndex);
            MDEF = database.stats[4].GetStatValue(myClass.classRingIndex) + database.stats[4].GetStatValue(myRace.raceRingIndex) + database.stats[4].GetStatValue(myAffinity.affinityRingIndex);
            CHA = database.stats[5].GetStatValue(myClass.classRingIndex) + database.stats[5].GetStatValue(myRace.raceRingIndex) + database.stats[5].GetStatValue(myAffinity.affinityRingIndex);
            LUK = database.stats[6].GetStatValue(myClass.classRingIndex) + database.stats[6].GetStatValue(myRace.raceRingIndex) + database.stats[6].GetStatValue(myAffinity.affinityRingIndex);
            maxSP = database.stats[7].GetStatValue(myClass.classRingIndex) + database.stats[7].GetStatValue(myRace.raceRingIndex) + database.stats[7].GetStatValue(myAffinity.affinityRingIndex);
            AGI = database.stats[8].GetStatValue(myClass.classRingIndex) + database.stats[8].GetStatValue(myRace.raceRingIndex) + database.stats[8].GetStatValue(myAffinity.affinityRingIndex);
            INT = database.stats[9].GetStatValue(myClass.classRingIndex) + database.stats[9].GetStatValue(myRace.raceRingIndex) + database.stats[9].GetStatValue(myAffinity.affinityRingIndex);
            ATK = database.stats[10].GetStatValue(myClass.classRingIndex) + database.stats[10].GetStatValue(myRace.raceRingIndex) + database.stats[10].GetStatValue(myAffinity.affinityRingIndex);
            maxHP = database.stats[11].GetStatValue(myClass.classRingIndex) + database.stats[11].GetStatValue(myRace.raceRingIndex) + database.stats[11].GetStatValue(myAffinity.affinityRingIndex);

            currentHP = maxHP;
            currentMP = maxMP;
            currentSP = maxSP;
        }
    }
}
