using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void enableUI()
    {
        mainMenu.SetActive(true);
    }
    public void enableMusic()
    {
        BGM.Play();
    }
}
