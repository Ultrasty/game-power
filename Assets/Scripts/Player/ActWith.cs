using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActWith : MonoBehaviour
{
    GameObject[] interactables;
    GameObject player;
    float height;
    float width;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        height = player.GetComponent<Collider2D>().bounds.size.y/2;
        width = player.GetComponent<Collider2D>().bounds.size.x/2;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            int length = interactables.Length;
            for(int i=0;i<length;i++)
            {
                Vector2 current_center = interactables[i].transform.position;
                pos = player.transform.position;
                float distance = Vector2.Distance(current_center, pos);
                if (distance*distance<(height*height+width*width))
                {
                    Debug.Log(distance);
                    Debug.Log(height * height + width * width);
                    interactables[i].BroadcastMessage("Interact");
                    break;
                }
            }
        }
    }

    void interactable_update()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }
}
