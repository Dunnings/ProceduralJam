using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RetryScript : MonoBehaviour 
{
    /// <summary>
    /// If retry selected load the main scene
    /// </summary>
    public void LoadLevel()
    {
        Application.LoadLevel("_David");
    }

    /// <summary>
    /// When enter text change col to grey
    /// </summary>
    /// <param name="retry"></param>
    public void OnHoverText(Text retry)
    {
        retry.color = Color.grey;
    }

    /// <summary>
    /// when exit text reset to white
    /// </summary>
    /// <param name="retry"></param>
    public void OnExitText(Text retry)
    {
        retry.color = Color.white;
    }

    /// <summary>
    /// when selected text set to black 
    /// </summary>
    /// <param name="retry"></param>
    public void OnClickText(Text retry)
    {
        retry.color = Color.black;
    }
}
