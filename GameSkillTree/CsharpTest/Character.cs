using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGgame
{
    class Character
    {
        public Character()
        {
            m_aSkills = new SkillParam[(int)SkillTag.TotalSkillCount];

            for (UInt16 ui16Iter = 0; ui16Iter < (int)SkillTag.TotalSkillCount; ui16Iter++)
                m_aSkills[ui16Iter] = new SkillParam();
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

        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public UInt32 PlayerId
        {
            get { return m_ui32PlayerId; }
            set { m_ui32PlayerId = value; }
        }

        private SkillParam[] m_aSkills;
        private string m_sName;
        private UInt32 m_ui32PlayerId;
    }
}
