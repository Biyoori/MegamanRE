using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotman : EnemyUnit
{

    Object bulletRef;

    public float cooldownCounterBase;
    public float changeArcBase;
    public float bodyDamage;

    float cooldownCounter;
    float changeArc;
    bool isCooldown = false;
    bool isLower = true;
    


    // Start is called before the first frame update
    void Start()
    {
        bulletRef = Resources.Load("EnemyBullet");
        cooldownCounter = cooldownCounterBase;
        changeArc = changeArcBase;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLower)
        {
            LowerArcShoot();
            changeArc -= Time.fixedDeltaTime;
            if(changeArc <= 0)
            {
                isLower = false;
                changeArc = changeArcBase;
            }
        }
        else
        {
            HighterArcShoot();
            changeArc -= Time.fixedDeltaTime;
            if (changeArc <= 0)
            {
                isLower = true;
                changeArc = changeArcBase;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MegaBusterBullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Megaman"))
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bodyDamage);
    }

    void LowerArcShoot()
    {
        if (!isCooldown)
        {
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.AddComponent<ShotmanBulletScript>();
            bullet.transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y, -2f);
            bullet.GetComponent<ShotmanBulletScript>().direction = new Vector2(Random.Range(-3, -3.5f), Random.Range(2, 2.5f));
            isCooldown = true;
        }
        else
        {
            cooldownCounter -= Time.fixedDeltaTime;
            if (cooldownCounter <= 0)
            {
                isCooldown = false;
                cooldownCounter = cooldownCounterBase;
            }
                
        }
            
    }

    void HighterArcShoot()
    {
        if (!isCooldown)
        {
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.AddComponent<ShotmanBulletScript>();
            bullet.transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y, -2f);
            bullet.GetComponent<ShotmanBulletScript>().direction = new Vector2(Random.Range(-0.5f, -1f), Random.Range(4, 4.5f));
            isCooldown = true;
        }
        else
        {
            cooldownCounter -= Time.fixedDeltaTime;
            if (cooldownCounter <= 0)
            {
                isCooldown = false;
                cooldownCounter = cooldownCounterBase;
            }
        }
    }
}
