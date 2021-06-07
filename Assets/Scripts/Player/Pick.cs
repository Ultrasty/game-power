using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] pickables;
            pickables = GameObject.FindGameObjectsWithTag("Pickable");
            int size = pickables.Length;
            Vector3 player_pos = player.transform.position;
            double width = player.GetComponent<Collider2D>().bounds.size.x;
            double height = player.GetComponent<Collider2D>().bounds.size.y;
            for (int i = 0; i < size; i++)
            {
                Vector3 pick_pos = pickables[i].transform.position;
                double distance = (player_pos.x - pick_pos.x) * (player_pos.x - pick_pos.x) + (player_pos.y - pick_pos.y) * (player_pos.y - pick_pos.y);
                double thresh = width * width + height * height;
                if(distance<= thresh/4)
                {
                    updateUI();
                    GameObject.Destroy(pickables[i]);
                    break;
                }
            }
        }

    }

    private void updateUI()
    {

    }
}
