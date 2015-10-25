using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGgame
{
    class Program
    {
        // Przykladowa funkcja ktora wyswietla wszystkie dostepne dane o umiejce. 
        static public void printSkillInfo(SkillTag a_eSkillTag)
        {
            //  Jesli pierwszy element z enum SkillTag jest mniejszy od podanego (on ma index 0), 
            // lub jesli ostatni element z enum'a SkillTag jest wiekszy od podanego 
            // (TotalSkillCount to zawsze ostatni element, wiec definiuje ile lacznie jest skilli w drzewku) 
            // to znaczy, ze podales jako argument zlego skilla i na bank nie znajdziesz go w drzewku
            if (SkillTag.MissingTag < a_eSkillTag && SkillTag.TotalSkillCount > a_eSkillTag)
            {
                Console.WriteLine("Name           : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).Name);
                Console.WriteLine("Description    : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).Description);
                Console.WriteLine("Tag            : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).Tag);
                Console.WriteLine("Parent tag     : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).ParentTag);
                Console.WriteLine("Root parent tag: {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).RootSkillTag);
                Console.WriteLine("Parent weight  : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).ParentWeight);
                Console.WriteLine("Is active      : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).IsSkillActive);
                Console.WriteLine("Icon path      : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).IconPath);
                Console.WriteLine("Support amount : {0}", m_CSkillsMngr.getSkillInfo(a_eSkillTag).Support.Count);

                // Jedna z dwoch opcji dobrania sie do supportow, obie tak samo dobre
                foreach (Skill.SupportSkill element in m_CSkillsMngr.getSkillInfo(a_eSkillTag).Support)
                {
                    Console.WriteLine("Support        : {0} [weight: {1}]", element.Tag, element.Weight);
                }

                //foreach or this
                //for (int i = 0; i < m_CSkillsMngr.getSkillInfo(a_eSkillTag).Support.Count; i++ )
                //{
                //    Console.WriteLine("Support        : {0} [weight: {1}]", m_CSkillsMngr.getSkillInfo(a_eSkillTag).Support[i].Tag, m_CSkillsMngr.getSkillInfo(a_eSkillTag).Support[i].Weight);
                //}

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Wrong skill tag passed");
            }

            return;
        }

        static void Main(string[] args)
        {
            m_CSkillsMngr = new SkillsMngr();
            m_CSkillsMngr.TechnicalInfoXmlPath = "W00t.xml";
            m_CSkillsMngr.LangDescritionXmlPath = "";

            m_CSkillsMngr.validateSkillTree();
            printSkillInfo(SkillTag.skill_2swordsingle);
            printSkillInfo(SkillTag.skill_agility);
            printSkillInfo(SkillTag.skill_axe);

            Character kati = new Character();
            kati.getSkillParam(SkillTag.skill_2sword).SkillValue = 5;
            Console.WriteLine(kati.getSkillParam(SkillTag.skill_2sword).SkillValue);
        }

        static private SkillsMngr m_CSkillsMngr;
    }
}
