using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBullet : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float time = 0;

    public bool faced_dir = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            GameObject go = Instantiate(bulletPrefab, transform.GetChild(1));
            go.tag = "Enimy";

            transform.GetChild(1).DetachChildren();
            Bullet bullet = go.GetComponent<Bullet>();
            int shoot_dir = faced_dir ? 1 : -1;
            bullet.targetVector = new Vector2(shoot_dir, 0);
            time = 0;
        }
    }
}
