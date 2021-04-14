using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceTrigger : MonoBehaviour
{
    private float timeStamp;
    private float startTime;
    private AudioSource voiceEntry;
    private bool voiceTrigger = true;
    private bool lineTrigger = true;

    public GameObject dialogBox;
    public Text dialogBoxText;
    public string lineText;
    public float lineTimer;

    private void Awake()
    {
        voiceEntry = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeStamp = Time.time;
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(false);
        }*/
        if(voiceTrigger == false)
        {
            if (timeStamp - startTime >= lineTimer && lineTrigger == true)
            {
                dialogBox.SetActive(false);
                lineTrigger = false;
            }
        }
        /*Debug.Log("now" + timeStamp);*/
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (voiceTrigger)
        {
            startTime = Time.time;
            /*Debug.Log("start" + startTime);*/
            voiceEntry.Play();
            dialogBoxText.text = lineText;
            dialogBox.SetActive(true);
            voiceTrigger = false;
        }
    }
}
