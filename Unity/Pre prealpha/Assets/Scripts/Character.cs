using System;
using UnityEngine;

namespace RPGgame
{
    class Character : MonoBehaviour
    {
        public Character()
        {
            m_aSkills = new SkillParam[(int)SkillTag.TotalSkillCount];

            for (UInt16 ui16Iter = 0; ui16Iter < (int)SkillTag.TotalSkillCount; ui16Iter++)
                m_aSkills[ui16Iter] = new SkillParam();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        //public Character(UInt32 a_sCharacterDBIndex){}

        public SkillParam getSkillParam(SkillTag a_oSkillTag)
        {
            return m_aSkills[(int)a_oSkillTag];
        }

        public bool recalculateSkillTree()
        {
            bool fRetVal = true;



            return fRetVal;
        }

        //TO-DO usunac accessor'y (?)
        public string Name
        {
            get { return m_CharName; }
            set { m_CharName = value; }
        }

        public UInt32 PlayerId
        {
            get { return m_PlayerId; }
            set { m_PlayerId = value; }
        }

        public SkillParam[] m_aSkills;
        public string m_CharName;
        public UInt32 m_PlayerId;
    }
}
