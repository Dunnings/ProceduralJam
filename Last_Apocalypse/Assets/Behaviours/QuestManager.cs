using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class QuestManager : MonoBehaviour {

    int day = 0;

    public Text m_dayCount;

    public static QuestManager Instance;

    public GameObject m_itemParent;
    public OxygenBar m_oxy;

    public Text m_questText;

    public List<string> m_requiredItems = new List<string>();
    
	//Ai stuffs
	public GameObject display, pc, speech;
	Text textComp;
	Animator anim, anim_speech;
	List<string> messages = new List<string>();
	bool spacebar;

	void Awake () {
        Instance = this;
	}
	
	void Start () {

		StreamReader inputFile = new StreamReader("Assets/Dialogue/test.txt");
		while (!inputFile.EndOfStream)
		{
			messages.Add(inputFile.ReadLine());
		}
		pc.gameObject.SetActive (true);
		anim = pc.GetComponent<Animator> ();
		textComp = display.GetComponent<Text>();
		textComp.text = "";
		anim_speech = speech.GetComponent<Animator> ();

        GenerateNewQuest();
    }
	// Update is called once per frame
	void Update()
	{
		spacebar = false;
		if (Input.GetMouseButtonDown (1)) {
			spacebar = true;
		}
		//if (Input.GetKeyDown(KeyCode.F1))
		//	Application.LoadLevel("_David");
	}

    public bool IsThisItemRequired(GameObject item)
    {
        for (int i = 0; i < m_requiredItems.Count; i++)
        {
            if (m_requiredItems[i] == item.name)
            {
                return true;
            }
        }
        return false;
    }

    public void GenerateNewQuest()
    {
		StartCoroutine (AnCouroutine());
        m_requiredItems.Clear();
        List<Item> potentialItems = new List<Item>();
        for (int i = 0; i < m_itemParent.transform.childCount; i++)
        {
            if (m_itemParent.transform.GetChild(i).gameObject.activeSelf && m_itemParent.transform.GetChild(i).GetComponent<Item>().isPlaced) {
                potentialItems.Add(m_itemParent.transform.GetChild(i).GetComponent<Item>());
            }
        }
        if (potentialItems.Count > 0)
        {
            m_requiredItems.Add(potentialItems[Random.Range(0, potentialItems.Count)].gameObject.name);
            m_questText.text = "- Collect a " + m_requiredItems[0];
            TimeManager.Instance.m_time = 6f;
            m_oxy.m_decreaseOxygen = true;
            m_oxy.m_oxygenPercent = 1f;
            day++;
            m_dayCount.text = "Day " + day;
        }
        else
        {
            //SUCCESS YOU WIN
            Application.LoadLevel("_Craig");
        }
    }

	IEnumerator AnCouroutine()
	{
		anim_speech.SetBool ("open", true);
		yield return new WaitForSeconds (2);
		for (int i = 0; i < messages.Count; i++)//For each packet of messages...
		{
			for (int j = 0; j < messages[i].Length; j++)//For each message...
			{
				if (messages[i][j] != '#')
					textComp.text += messages[i][j];
				
				if (messages[i][j] == '#')
				{
					anim.SetBool("talking", false);
					while(!spacebar)
						yield return null;
					
					textComp.text = ""; //Reset text
				}
				anim.SetBool("talking", true);
				yield return new WaitForSeconds(0.025f);
			}
		}
		anim.SetBool("talking", false);
		anim_speech.SetBool ("open", false);
		yield return new WaitForSeconds (2);
		pc.gameObject.SetActive (false);
		//Application.LoadLevel("_David");
	}

    public void FailedQuest()
    {
        //GAME OVER
    }
}
