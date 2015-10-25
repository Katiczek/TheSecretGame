/*
 * To obtain any information about skill call getSkillInfo(SkillTag skillYouWantToKnow) and after . call any member listed below.
 * 
 * To set and (automaticly) parse xml files with skills information simply set:
 * -LangDescritionXmlPath
 * -TechnicalInfoXmlPath
 * Class will automaticly pare xml file. Warning: it's not wise to do it frequently, or any time in code - file maybe is not so big, 
 * but it's always few sec/milisec freeze for program. Do it on init of game and when player want to change language settings
 * 
 * See Skill class file for Skill structure
 *   
 * SkillTag is enum with all possible to obtain skills in game -> look to SkillTag file for more details
 * 
 */

using System;

using System.Xml;
using System.IO;

namespace RPGgame
{
    class SkillsMngr
    {
        /* Constructor allocates memory for whole skill tree information (number of elements is defined by number of elements in SkillTag enum) 
         */ 
        public SkillsMngr()
        {
            m_aSkillsInfo = new Skill[(int)SkillTag.TotalSkillCount];

            for (ushort usIter = 0; usIter < (int)SkillTag.TotalSkillCount; usIter++)
                m_aSkillsInfo[usIter] = new Skill();
        }

        /* Validates skill tree information. Validation checks if any of:
         * -Name
         * -Description
         * -Tag
         * -Parent tag
         * -Root parent tag
         * -Icon path
         * is missing (empty string). 
         * 
         * Returns 0 if no problems found in skill tree. Otherwise number of error is returned
         */
        public uint validateSkillTree()
        {
            uint uiErrorCounter = 0;

            if (null == m_aSkillsInfo)
            {
                uiErrorCounter = 1;
                Console.WriteLine("[ERR] CSkillsMngr:validateSkillTree: skill tree not initilized");
            }
            else
            {
                Console.WriteLine("CSkillsMngr:validateSkillTree: Analyzing skill tree...");

                if (m_aSkillsInfo.Length != (int)SkillTag.TotalSkillCount)
                {
                    uiErrorCounter++;
                    Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill tree don't contain all skill from enum list");
                }

                for (int i = 1; i < (int)SkillTag.TotalSkillCount; i++)
                {
                    if ("" == m_aSkillsInfo[i].Description)
                    {
                        Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill {0}[{1}] is missing Description", (SkillTag)i, i);
                        uiErrorCounter++;
                    }
                    if ("" == m_aSkillsInfo[i].Name)
                    {
                        Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill {0}[{1}] is missing Name", (SkillTag)i, i);
                        uiErrorCounter++;
                    }
                    if (SkillTag.MissingTag == m_aSkillsInfo[i].Tag)
                    {
                        Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill {0}[{1}] is missing Tag", (SkillTag)i, i);
                        uiErrorCounter++;
                    }
                    if (SkillTag.MissingTag == m_aSkillsInfo[i].RootSkillTag)
                    {
                        Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill {0}[{1}] is missing RootParent", (SkillTag)i, i);
                        uiErrorCounter++;
                    }
                    if (SkillTag.MissingTag == m_aSkillsInfo[i].ParentTag)
                    {
                        Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill {0}[{1}] is missing Parent", (SkillTag)i, i);
                        uiErrorCounter++;
                    }
                    if ("" == m_aSkillsInfo[i].IconPath)
                    {
                        Console.WriteLine("[WARN] CSkillsMngr:validateSkillTree: skill {0}[{1}] is missing IconPath", (SkillTag)i, i);
                        uiErrorCounter++;
                    }
                }
            }
            Console.WriteLine("CSkillsMngr:validateSkillTree: Summary {0} errors in skill tree found", uiErrorCounter);

            return uiErrorCounter;
        }

        /* Returns Skill object
         */
        public Skill getSkillInfo(SkillTag a_SkillTag)
        {
            return m_aSkillsInfo[(int)a_SkillTag];
        }

        /* Path to xml file which contains technical information about skills in game
         * Automaticly parses file when it's set
         */
        public string TechnicalInfoXmlPath
        {
            get { return m_sSkillsTechnicalInfoXmlPath; }
            set
            {
                m_sSkillsTechnicalInfoXmlPath = value;
                parseSkillsTechnicalInfoXml();
            }
        }

        /* Path to xml file which contains skills language descriptions and names 
         * Automaticly parses file when it's set
         */
        public string LangDescritionXmlPath
        {
            get { return m_sSkillsLangDescritionXmlPath; }
            set
            {
                m_sSkillsLangDescritionXmlPath = value;
                parseSkillsLangDescritionXml();
            }
        }

        /* Converts string to skillTag if et exists with exact same name.
         * Returns MissingTag if no skill tag with passed name was found
         */
        private SkillTag convertStringToSkillTag(String a_sTagToConvert)
        {
            SkillTag SkillTag = SkillTag.MissingTag;
            try
            {
                SkillTag = (SkillTag)Enum.Parse(typeof(SkillTag), a_sTagToConvert);
                if (!Enum.IsDefined(typeof(SkillTag), SkillTag))
                    Console.Error.WriteLine("[ERR] SkillsTechnicalInfo:convertStringToSkillTag: {0} is not an underlying value of the SkillTag enumeration.", a_sTagToConvert);
            }
            catch (ArgumentException)
            {
                Console.Error.WriteLine("[ERR] SkillsTechnicalInfo:convertStringToSkillTag: '{0}' is not a member of the SkillTag enumeration.", a_sTagToConvert);
            }

            return SkillTag;
        }

        /* Parses xml with technical info about skills in game
         * m_sSkillsTechnicalInfoXmlPath must be set
         * Returns 0 if error occured, 1 otherwise
         */
        private short parseSkillsTechnicalInfoXml()
        {
            short fRetVal = 1;

            if (System.IO.File.Exists(m_sSkillsTechnicalInfoXmlPath))   //namespace System.IO
            {
                string xmlInfo = "";

                try
                {
                    using (StreamReader sr = new StreamReader(m_sSkillsTechnicalInfoXmlPath))
                    {
                        xmlInfo = sr.ReadToEnd();
                    }

                    SkillTag skillTag = SkillTag.MissingTag;
                    SkillTag SupportSkillTag = SkillTag.MissingTag;

                    using (XmlReader reader = XmlReader.Create(new StringReader(xmlInfo)))
                    {
                        while (reader.ReadToFollowing("Skill"))
                        {
                            reader.ReadToFollowing("Tag");//konwersja z string to enum https://msdn.microsoft.com/pl-pl/library/system.enum.parse(v=vs.110).aspx

                            skillTag = convertStringToSkillTag(reader.ReadElementContentAsString());
                            if (SkillTag.MissingTag != skillTag)
                            {
                                m_aSkillsInfo[(int)skillTag].Tag = skillTag;
                                reader.ReadToFollowing("Parent");
                                m_aSkillsInfo[(int)skillTag].ParentTag = convertStringToSkillTag(reader.ReadElementContentAsString());
                                reader.ReadToFollowing("ParentWeight");
                                m_aSkillsInfo[(int)skillTag].ParentWeight = reader.ReadElementContentAsFloat();
                                reader.ReadToFollowing("RootParent");
                                m_aSkillsInfo[(int)skillTag].RootSkillTag = convertStringToSkillTag(reader.ReadElementContentAsString());
                                reader.ReadToFollowing("IsActive");
                                m_aSkillsInfo[(int)skillTag].IsSkillActive = reader.ReadElementContentAsBoolean();
                                reader.ReadToFollowing("IconPath");
                                m_aSkillsInfo[(int)skillTag].IconPath = reader.ReadElementContentAsString();
                                reader.ReadToFollowing("Supports");
                                UInt16 ui16SupportAmount = UInt16.Parse(reader.GetAttribute(0));
                                for (UInt16 i = 0; i < ui16SupportAmount; i++)
                                {
                                    reader.ReadToFollowing("SupportTag");
                                    SupportSkillTag = convertStringToSkillTag(reader.ReadElementContentAsString());
                                    reader.ReadToFollowing("SupportWeight");
                                    m_aSkillsInfo[(int)skillTag].Support.Add(new Skill.SupportSkill(SupportSkillTag, reader.ReadElementContentAsFloat()));
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    fRetVal = 0;
                    Console.Error.WriteLine("[ERR] CSkillsMngr:parseSkillsTechnicalInfoXml: Exception occured while reading {0} file\n{1}", m_sSkillsTechnicalInfoXmlPath, e.ToString());
                }
            }
            else
            {
                fRetVal = 0;
                Console.Error.WriteLine("[ERR] CSkillsMngr:parseSkillsTechnicalInfoXml: File {0} don't exists!", m_sSkillsTechnicalInfoXmlPath);
            }

            return fRetVal;
        }

        /* Parses xml with language info about skills in game
         * m_sSkillsLangDescritionXmlPath must be set
         * Returns 0 if error occured, 1 otherwise
         */
        private short parseSkillsLangDescritionXml()
        {
            short fRetVal = 1;

            if (System.IO.File.Exists(m_sSkillsLangDescritionXmlPath))   //namespace System.IO
            {
                string xmlInfo = "";

                try
                {
                    using (StreamReader sr = new StreamReader(m_sSkillsLangDescritionXmlPath))
                    {
                        xmlInfo = sr.ReadToEnd();
                    }

                    SkillTag skillTag = SkillTag.MissingTag;

                    using (XmlReader reader = XmlReader.Create(new StringReader(xmlInfo)))
                    {
                        while (reader.ReadToFollowing("Skill"))
                        {
                            reader.ReadToFollowing("Tag");
                            skillTag = convertStringToSkillTag(reader.ReadElementContentAsString());

                            if (SkillTag.MissingTag != skillTag)
                            {
                                reader.ReadToFollowing("Name");
                                m_aSkillsInfo[(int)skillTag].Name = reader.ReadElementContentAsString();
                                reader.ReadToFollowing("Description");
                                m_aSkillsInfo[(int)skillTag].Description = reader.ReadElementContentAsString();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    fRetVal = 0;
                    Console.Error.WriteLine("[ERR] CSkillsMngr:parseSkillsLangDescritionXml: Exception occured while reading {0} file\n{1}", m_sSkillsLangDescritionXmlPath, e.ToString());
                }
            }
            else
            {
                fRetVal = 0;
                Console.Error.WriteLine("[ERR] CSkillsMngr:parseSkillsLangDescritionXml: File {0} don't exists!", m_sSkillsLangDescritionXmlPath);
            }

            return fRetVal;
        }

        private Skill[] m_aSkillsInfo; 
        private string m_sSkillsTechnicalInfoXmlPath;
        private string m_sSkillsLangDescritionXmlPath;   
    }
}
