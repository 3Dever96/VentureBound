using UnityEngine;

namespace VentureBound.Database
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Database/Skill")]
    public class Skill : ScriptableObject
    {
        public string skillName;
        [TextArea]
        public string skillDescription;
        public int skillRingIndex;
        public SkillType skillType;
        public Skill[] parentSkills;
        public int[] skillCosts = { 1, 2, 3, 4, 5, 6, 7 };

        public int GetSkillCost(int ringIndex)
        {
            if (parentSkills.Length == 0)
            {
                int diff = Mathf.Abs(ringIndex - skillRingIndex);
                int trueIndex = Mathf.Min(diff, 12 - diff);

                return skillCosts[trueIndex];
            }
            else
            {
                int value1 = parentSkills[0].GetSkillCost(ringIndex) + 1;
                int value2 = parentSkills[1].GetSkillCost(ringIndex) + 1;

                return Mathf.Min(value1, value2);
            }
        }
    }

    public enum SkillType
    {
        Active,
        Passive
    }
}
