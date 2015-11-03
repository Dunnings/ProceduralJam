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

    public List<GameObject> m_requiredItems = new List<GameObject>();
    
	//Ai stuffs
	public GameObject display, pc, speech, CharMover;
	Text textComp;
	Animator anim, anim_speech;
	CharMovement movementScript;
	List<string> messages = new List<string>();
	bool spacebar;

	void Awake () {
        Instance = this;
	}
	
	void Start () {
		movementScript = CharMover.GetComponent<CharMovement>();
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
            if (m_requiredItems[i].name == item.name)
            {
                return true;
            }
        }
        return false;
    }

    public void GenerateNewQuest()
    {
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
            m_requiredItems.Add(potentialItems[Random.Range(0, potentialItems.Count)].gameObject);
            m_questText.text = "- Collect a " + m_requiredItems[0].name;
            TimeManager.Instance.m_time = 6f;
            m_oxy.m_decreaseOxygen = true;
            m_oxy.m_oxygenPercent = 1f;
            day++;
            m_dayCount.text = "Day " + day;
			StartCoroutine (AnCouroutine());
        }
        else
        {
            //SUCCESS YOU WIN
            Application.LoadLevel("_Craig");
        }
    }

	IEnumerator AnCouroutine()
	{
		string filepath = "Assets/Dialogue/Day" + day + ".txt";
		StreamReader inputFile = new StreamReader(filepath);
		messages.Clear ();
		while (!inputFile.EndOfStream)
		{
			messages.Add(inputFile.ReadLine());
		}
		
		movementScript.maxSpeed = 100.0f;
		anim_speech.SetBool ("open", true);
		pc.gameObject.SetActive (true);
		yield return new WaitForSeconds (1.5f);
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
		movementScript.maxSpeed = 0.3f;
		yield return new WaitForSeconds (2);
		pc.gameObject.SetActive (false);
		//Application.LoadLevel("_David");
	}

    public void FailedQuest()
    {
        //GAME OVER
    }

    public void PickedUpObject()
    {
        m_questText.text = "- Return the " + m_requiredItems[0].name + " to the base";
    }

    public void DroppedObject()
    {
        m_questText.text = "- Collect a " + m_requiredItems[0].name;
    }
}
