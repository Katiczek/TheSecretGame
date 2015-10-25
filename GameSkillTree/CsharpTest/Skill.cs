/*
 * Skill structure: 
 *   SkillTag Tag;      
 *   SkillTag ParentTag;
 *   SkillTag RootSkillTag;
 *   float ParentWeight;
 *   bool IsActive;
 *   string IconPath;
 *   string Name;                // in one, selected language
 *   string Description;         // in one, selected language
 *   SkillTag Support[i].Tag     // where i is odrinal number of support
 *   float Support[i].Weight     // where i is odrinal number of support
 *          
 * To get number of supports:
 *   Support.Count
 */
  
using System;
using System.Collections.Generic;

namespace RPGgame
{
    public class Skill
    {
        public Skill()
        {
            m_eTag = SkillTag.MissingTag;
            m_eParentTag = SkillTag.MissingTag;
            m_fParentWeight = 0;
            m_eRootSkillTag = SkillTag.MissingTag;
            m_bIsActive = false;
            m_lSupportSkills = new List<SupportSkill>();
            m_sIconPath = "";
            m_sName = "";
            m_sDescription = "";
        }

        // --------------------  Accessors --------------------  https://msdn.microsoft.com/en-us/library/aa287786(v=vs.71).aspx

        public SkillTag Tag
        {
            get { return m_eTag; }
            set { m_eTag = value; }
        }

        public SkillTag ParentTag
        {
            get { return m_eParentTag; }
            set { m_eParentTag = value; }
        }

        public SkillTag RootSkillTag
        {
            get { return m_eRootSkillTag; }
            set { m_eRootSkillTag = value; }
        }

        public float ParentWeight
        {
            get { return m_fParentWeight; }
            set { m_fParentWeight = value; }
        }

        public bool IsSkillActive
        {
            get { return m_bIsActive; }
            set { m_bIsActive = value; }
        }

        public string IconPath
        {
            get { return m_sIconPath; }
            set
            {
                //To-do validate path to icon (do exist?), load image to memory?
                m_sIconPath = value;
            }
        }

        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }
            
        public IList<SupportSkill> Support
        {
            get { return m_lSupportSkills; }
        }
        // -------------------- / Accessors --------------------

        public class SupportSkill
        {            
            public SupportSkill()
            {
                m_eTag = SkillTag.MissingTag;
                m_fWeight = 0;
            }

            public SupportSkill(SkillTag a_eTag, float a_fWeight)
            {
                m_eTag = a_eTag;
                m_fWeight = a_fWeight;
            }

            // --------------------  Accessors --------------------
            public SkillTag Tag
            {
                get { return m_eTag; }
                set { m_eTag = value; }
            }

            public float Weight
            {
                get { return m_fWeight; }
                set { m_fWeight = value; }
            }
            // -------------------- / Accessors --------------------

            private SkillTag m_eTag;
            private float m_fWeight;
        }
        
        //private
        private SkillTag m_eTag;      
        private SkillTag m_eParentTag;
        private SkillTag m_eRootSkillTag;
        private List<SupportSkill> m_lSupportSkills;
        private float m_fParentWeight;
        private bool m_bIsActive;
        private string m_sIconPath;
        private string m_sName;
        private string m_sDescription;
    }
    
}
