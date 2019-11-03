using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotmanBulletScript : EnemyBullet
{
    Rigidbody2D rb2d;
    PlayerController playerCon;

    public Vector2 direction;

    float bulletDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(direction, ForceMode2D.Impulse);
        playerCon = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("SelfDestroy", 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Megaman"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
