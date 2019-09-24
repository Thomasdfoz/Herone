using UnityEngine;
using UnityEngine.AI;

/* Base class that player and enemies can derive from to include stats. */
public abstract class CharacterStats : MonoBehaviour
{
    [Space]
    [Header("Raça")]
    public Race race;
    // Health   
    [Header("Atributos")]
    public PlayerConnection health;
    public Stat damage;
    public Stat armor;
    public float moveSpeed;
    [Space]
    public float rangeAttack;
    public bool ranged;
    public int missPercentage;
    public int criticPercentage;
    [Space]
    [Header("Outros")]
    public GameObject projectile;
    public bool die = false;
    public bool activeTemp;
    public float temp;


    protected NavMeshAgent navMeshAgent;
    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health.Initialize();
        health.CurrentVal = health.MaxVal;
        navMeshAgent.speed = moveSpeed / 100;
    }

    private void FixedUpdate()
    {
        if (activeTemp)
            temp += Time.deltaTime;
    }


    // Damage the character
    public abstract void TakeDamage(GameObject attacker, float damage, InfAtk inf, TypeDamage typeDamage);    
    public void BurningDamage(GameObject attacker, float damage, TypeDamage typeDamage, float cowndown)
    {
        activeTemp = true;
        if (temp >= cowndown)
        {
            TakeDamage(attacker, damage, InfAtk.normal, typeDamage);
            temp = 0;
        }

    }

    public virtual void Die(GameObject killer)
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
        die = true;
    }
    protected float DefeseValue(Stat defese)
    {
        float total;
        float divisor = 100 + defese.GetValue();
        total = 100 / divisor;
        return total;

    }

}
