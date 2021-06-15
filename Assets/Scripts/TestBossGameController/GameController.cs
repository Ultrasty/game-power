using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {

        GUI.Box(new Rect(10, 10, 100, 90), "Debug Menu");

        if (GUI.Button(new Rect(20, 40, 80, 20), "win"))
        {
            success();
        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "lose"))
        {
            
        }
    }

    public void success()
    {

    }

}
