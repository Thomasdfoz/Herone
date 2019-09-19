using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float bulletDamage = 10;
    public float bulletSpeed = 100f;
    Rigidbody2D rbBullet;

    public float bulletTimeLife = 5f;
    float bulletTimeCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        rbBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletTimeCount >= bulletTimeLife)
        {
            Destroy(this.gameObject);
        }

        bulletTimeCount += Time.deltaTime;
        
    }

    
    void BulletDestroy()
    {
        Destroy(this.gameObject);
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }

        Destroy(this.gameObject);
    }

}
