using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectmove : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 1000;
    [SerializeField, Tooltip("up or down")]
    int dir = -1;

    public float lifetime = 3f;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    private float time = 0;

    private Vector2 velocity;

    private int first = 0;

    // Start is called before the first frame update
    void Start()
    {
        velocity.x = 0;
        velocity.y = speed*dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        time += Time.deltaTime;
        if (time >= lifetime+first*lifetime)
        {
            dir = -dir;
            velocity.y = dir*speed;
            time = 0;
            first = 1;
        }

    }
}
