using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicScript : MonoBehaviour
{
    public GameObject dono;
    ParticleSystem particle;
    public float damage;
    public bool activeDamage;
    public List<GameObject> list;
    public float temp;
    public float cd;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        list.Clear();
    }    

    // Update is called once per frame
    void Update()
    {       
        temp += Time.deltaTime;

        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
        if (temp >= cd)
        {
            if (list.Count > 0)
            {
                MagicDamage();
            }
            temp = 0;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human")
        {
            if (!list.Contains(other.gameObject))
            {
                list.Add(other.gameObject);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            if (list.Contains(other.gameObject))
            {
                list.Remove(other.gameObject);
            }
        }

    }
    void MagicDamage()
    {
        foreach (GameObject enemy in list)
        {
            if (enemy == null)
            {
                list.Remove(enemy);
            }
            else
            {
                enemy.GetComponent<EnemyStats>().TakeDamage(dono, damage, InfAtk.normal, TypeDamage.magic);
            }
        }
    }
}
