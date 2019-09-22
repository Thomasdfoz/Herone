using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicScript : MonoBehaviour
{
    public GameObject dono;
    ParticleSystem particle;
    public float damage;    
    public float cowndown;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();        
    }

    // Update is called once per frame
    void LateUpdate()
    { 
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStats>())
        {
            if (other.GetComponent<CharacterStats>().race != dono.GetComponent<CharacterStats>().race)
            {
                other.GetComponent<CharacterStats>().BurningDamage(dono, damage, TypeDamage.magic, cowndown);
            }
        }
       

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterStats>())
        {
            if (other.GetComponent<CharacterStats>().race != dono.GetComponent<CharacterStats>().race)
            {
                other.GetComponent<CharacterStats>().BurningDamage(dono, damage, TypeDamage.magic, cowndown);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterStats>())
        {

            if (other.GetComponent<CharacterStats>().race != dono.GetComponent<CharacterStats>().race)
            {
                other.GetComponent<EnemyStats>().activeTemp = false;
                other.GetComponent<EnemyStats>().temp = 0;
            }
        }

    }
    void MagicDamage(GameObject enemy)
    {
        float temp = 0;
        temp += Time.deltaTime;
        Debug.Log(temp);
        if (temp >= cowndown)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(dono, damage, InfAtk.normal, TypeDamage.magic);
        }
    }
}
