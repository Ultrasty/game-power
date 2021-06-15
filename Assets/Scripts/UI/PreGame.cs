using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreGame : MonoBehaviour
{
    public Text dialogue;
    public Text enter;
    private ArrayList diaList = new ArrayList();
    private int num = 0;
    private AudioSource voiceEntry;
    // Start is called before the first frame update
    void Start()
    {
        voiceEntry = GetComponent<AudioSource>();
        dialogue.text = "Cold steel haven't been assassins' favorite weapon in a cyberpunk city.";
        diaList.Add("They prefer big guns.Blow their targets sky-high.");
        diaList.Add("Well,not in my line of work.");
        diaList.Add("Because you can't kill ghosts and goblins with a gun...");
        diaList.Add("You kill them with Katana.");
        enter.text = "Press enter to wake up";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (num == diaList.Count)
            {
                voiceEntry.Play();
                Invoke("cutScene", 3);
            }
            dialogue.text = (string)diaList[num];
            num++;
        }
    }
    void cutScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
