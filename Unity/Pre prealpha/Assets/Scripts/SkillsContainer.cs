using UnityEngine;
using System.Collections;
using RPGgame;

public class SkillsContainer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        m_SkillsMngr = new SkillsMngr();
        m_SkillsMngr.TechnicalInfoXmlPath = "C:\\Users\\Magda\\Desktop\\TheSecretGame\\Unity\\Pre prealpha\\Assets\\Scripts\\W00t.xml";
        m_SkillsMngr.LangDescritionXmlPath = "";

        m_SkillsMngr.validateSkillTree();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public SkillsMngr m_SkillsMngr;
}
