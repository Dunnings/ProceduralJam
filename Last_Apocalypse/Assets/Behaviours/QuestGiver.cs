using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour
{
    public static QuestGiver Instance;
    
	public GameObject display, pc;
	Text textComp;
	Animator anim;
	List<string> messages = new List<string>();
	bool spacebar;

    void Awake() {
        Instance = this;

    }

    // Use this for initialization
    void Start()
    {
		Setup ("Assets/Dialogue/Day1_scene1.txt");
		StartCoroutine (AnCouroutine());
    }

    // Update is called once per frame
    void Update()
    {
		spacebar = false;
		if (Input.GetMouseButtonDown (1)) {
			spacebar = true;
		}
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Application.LoadLevel("_David");
        }
    }

	IEnumerator AnCouroutine()
	{
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
        Application.LoadLevel("_David");
	}

    void Setup(string filepath)
    {
        StreamReader inputFile = new StreamReader(filepath);

        while (!inputFile.EndOfStream)
        {
            messages.Add(inputFile.ReadLine());
        }
		anim = pc.GetComponent<Animator> ();
		textComp = display.GetComponent<Text>();
		textComp.text = "";
    }
}
//Press button to use quest giver
//QG outputs text depending on if player has spoken to QG before + what day it is
//For text, grab word, output unless there it goes over the character limit, in which case, add a linebreak, then do it.
