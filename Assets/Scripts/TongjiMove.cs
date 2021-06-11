using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongjiMove : MonoBehaviour
{
    // Start is called before the first frame update
    void des()
    {
        Invoke("destroyself", 2f);
    }

    void destroyself()
    {
        Destroy(gameObject);
    }
}
