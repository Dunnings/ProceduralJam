using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour
{

    public int CharacterLimitPerLine = 20;
    public int MaxLines = 5;

    public GameObject display;

    // Use this for initialization
    void Start()
    {
        OnClickOrWhatever();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClickOrWhatever()
    {
        #region Get message
        List<string> messages = new List<string>();
        StreamReader inputFile = new StreamReader("Assets/Dialogue/Day1_scene1.txt");

        while (!inputFile.EndOfStream)
        {
            messages.Add(inputFile.ReadLine());
        }
        #endregion

        //TextMesh textComp = display.GetComponent<TextMesh>();
		//GUIText textComp2 = display.GetComponent<GUIText> ();
		Text textComp = display.GetComponent<Text>();
		textComp.text = "";

        #region Text Loop
        int lineCount = 0;
        for (int i = 0; i < messages.Count; i++)//For each packet of messages...
        {
            if (lineCount > MaxLines)//If we've filled up the speech bubble...
            {                //Wait for button press
                textComp.text = ""; //Reset text
                lineCount = 0;
            }


            for (int j = 0; j < messages[i].Length; j++)//For each message...
            {
				/*string word = "", output = "";
				int jPos = j;
				bool skip = false;
                while (messages[i].Length > CharacterLimitPerLine)//If the message can't fit on the line...
                {
					int temp = messages[i].Length;
                    //i = line number of text file
                    //j = position along line
					if (messages[i][jPos] != ' ' && !skip)//If no space character
                    {
                        word += messages[i][jPos]; //Add char to word
                        jPos++;
						if (jPos > temp-2)
							skip = true;
						else
							skip = false;

                    }
                    else //If we hit a space character / end of word!
                    {
                        if (output.Length + word.Length > CharacterLimitPerLine)//If we can't fit the word in with the rest of the message...
                        {
                            //Take the existing message, trim off the end
                            string otherOutput = "";

							int start = output.Length;
							int stop = messages[i].Length;

                            for (int o = start; o < stop; o++)//msgs[i].L - output.L = length of the rest of the string
                            {
                                otherOutput += messages[i][o];
                            }
                            messages.Insert(i + 1, otherOutput);
                            messages[i] = output;
                        }
                        else
                        {
							output += word;
							output += ' ';
                            word = "";
							jPos++;
							if (jPos > messages[i].Length-2)
								skip = true;
							else
								skip = false;
                        }
                    }
                }*/
                //yield WaitForSeconds(0.5);
                textComp.text += messages[i][j];
            }
            textComp.text += '\n';
            lineCount++;
        }
        #endregion
    }
}



//Press button to use quest giver
//QG outputs text depending on if player has spoken to QG before + what day it is
//For text, grab word, output unless there it goes over the character limit, in which case, add a linebreak, then do it.
