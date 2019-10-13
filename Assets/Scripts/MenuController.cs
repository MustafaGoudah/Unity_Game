using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(Index);
        Debug.Log("1");
    }


    public void Quit()
    {
       

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
