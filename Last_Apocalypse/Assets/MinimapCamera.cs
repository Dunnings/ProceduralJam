using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {
    
    public Shader unlitShader;

    void Start()
    {
        //unlitShader = Shader.Find("Unlit/Texture");
        GetComponent<Camera>().SetReplacementShader(unlitShader, "");
        gameObject.SetActive(false);
    }
}
