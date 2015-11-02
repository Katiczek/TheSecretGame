using System;

namespace RPGgame
{
    public class SkillParam
    {
        public SkillParam()
        {
            m_i16SkillValue = 0;
            m_ui16SkillProgress = 0;
            m_i16Modifier = 0;
        }

        public SkillParam(Int16 a_i16SkillValue, UInt16 a_ui16SkillProgress, Int16 a_i16Modifier)
        {
            m_i16SkillValue = a_i16SkillValue;
            m_ui16SkillProgress = a_ui16SkillProgress;
            m_i16Modifier = a_i16Modifier;
        }

        public Int16 SkillValue
        {
            get { return m_i16SkillValue; }
            set { m_i16SkillValue = value; }
        }

        public UInt16 SkillProgress
        {
            get { return m_ui16SkillProgress; }
            set { m_ui16SkillProgress = value; }
        }

        public Int16 Modifier
        {
            get { return m_i16Modifier; }
            set { m_i16Modifier = value; }
        }

        public Int16 m_i16SkillValue;
        public UInt16 m_ui16SkillProgress;
        public Int16 m_i16Modifier;
    }
}
