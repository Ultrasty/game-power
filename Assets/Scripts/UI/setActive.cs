using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActive : MonoBehaviour
{
    public List<GameObject> objects;
    public GameObject portal;
    public GameObject collectMask;
    private bool ifComplete;
    // Start is called before the first frame update
    void Start()
    {
        ifComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i])
            {
                ifComplete = false;
                break;
            }
            else
            {
                ifComplete = true;
            }
        }
        if (ifComplete)
        {
            portal.gameObject.SetActive(true);
            collectMask.gameObject.SetActive(false);
        }
    }
}
