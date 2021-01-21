using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.5f);
    }
}
