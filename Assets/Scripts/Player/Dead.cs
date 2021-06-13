using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    GameObject map;
    GameObject player;
    public AudioSource dead_audio;
    public GameObject death_UI;
    public float death_time_remain = 3f;
    private bool is_dead = false;
    Scene scene;
    Vector2 start_point;
    double y_below;
    // Start is called before the first frame update
    void Start()
    {
        death_UI.SetActive(false);
        map = GameObject.FindGameObjectWithTag("Map");
        player = GameObject.FindGameObjectWithTag("Player");
        scene = SceneManager.GetActiveScene();
        start_point = player.transform.position;
        y_below = map.GetComponent<Renderer>().bounds.center.y - map.GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_dead && player.transform.position.y<y_below)
        {
            //player.transform.position = start_point;
            is_dead = true;
            death_UI.SetActive(true);
            dead_audio.Play();
            
        }
        if(is_dead)
        {
            death_time_remain -= Time.deltaTime;
            if(death_time_remain<=0)
                SceneManager.LoadScene(scene.name);
        }
    }
}
