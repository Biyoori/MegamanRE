using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb2d;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("SelfDestroy", 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = direction;

    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
