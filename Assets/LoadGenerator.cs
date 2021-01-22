using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadGenerator : GameMenu
{
    void Start()
    {
	
        
    }
    void Update()
    {
	if (Input.GetKey("f")) SceneManager.LoadScene("Generator");
    }	
   
}
