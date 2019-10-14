using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    public bool isMuted;
    // Start is called before the first frame update
    void Start()
    {
        isMuted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mute()
    {
        isMuted = !isMuted;
        Debug.Log(isMuted);
        AudioListener.pause = isMuted;
    }
}
