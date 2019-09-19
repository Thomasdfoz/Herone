using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject alvo;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (alvo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == alvo)
        {
            Destroy(gameObject);
        }
       
    }
}
