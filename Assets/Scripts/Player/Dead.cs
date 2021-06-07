using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    GameObject map;
    GameObject player;
    Scene scene;
    Vector2 start_point;
    double y_below;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        player = GameObject.FindGameObjectWithTag("Player");
        scene = SceneManager.GetActiveScene();
        start_point = player.transform.position;
        y_below = map.GetComponent<Renderer>().bounds.center.y - map.GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y<y_below)
        {
            //player.transform.position = start_point;
            SceneManager.LoadScene(scene.name);
        }
    }
}
