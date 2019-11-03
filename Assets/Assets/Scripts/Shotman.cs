using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotman : EnemyUnit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MegaBusterBullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
            Debug.Log("Damage Taken");
        }
    }
}
