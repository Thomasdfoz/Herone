using Assets.Game.Scripts;
using Assets.MyGame.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    [SerializeField]
    private float attackSpeed;
    private float attackCooldown = 0f;

    public float attackDelay;

    [SerializeField]
    private GameObject projectileExit;

    public event System.Action OnAttack;

    CharacterStats myStats;
    NavMeshAgent agent;


    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (attackSpeed > 100)
        {
            attackSpeed = 100;
        }
    }
    public void AttackRanged(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            if (!targetStats.die)
            {
                this.gameObject.GetComponent<CharacterAnimator>().AttackAnimation();
                StartCoroutine(DoProjectile(myStats.gameObject, 0.4f, targetStats));
                if (OnAttack != null)
                    OnAttack();
            }
            attackCooldown = 100f / attackSpeed;
        }

    }
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            if (!targetStats.die)
            {
                this.gameObject.GetComponent<CharacterAnimator>().AttackAnimation();
                StartCoroutine(DoDamage(myStats.gameObject, targetStats, attackDelay, myStats.damage.GetValue()));
                if (OnAttack != null)
                    OnAttack();
                attackCooldown = 100f / attackSpeed;
            }
        }
    }

    IEnumerator DoDamage(GameObject attacker, CharacterStats stats, float delay, int damage)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(delay);
        bool miss = CharacterManager.RandomBool(myStats.missPercentage, 100f);
        InfAtk inf;
        if (!miss)
        {
            bool critico = CharacterManager.RandomBool(myStats.criticPercentage, 100f);
            if (critico)
            {
                damage = CharacterManager.Critical(damage, 200);
                inf = InfAtk.critic;
            }
            else
            {
                damage = CharacterManager.Damage(damage);
                inf = InfAtk.normal;
            }
        }
        else
        {
            damage = 0;
            inf = InfAtk.miss;
        }
        stats.TakeDamage(attacker, damage, inf, TypeDamage.physic);
        agent.isStopped = false;

    }
    IEnumerator DoProjectile(GameObject attacker, float temp, CharacterStats targetStats)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(temp);
        if (!targetStats.die)
        {
            GameObject projectile = Instantiate(myStats.projectile, projectileExit.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().alvo = targetStats.gameObject;
        }
        float distance = Vector3.Distance(transform.position, targetStats.transform.position);
        agent.isStopped = false;
        StartCoroutine(DoDamage(attacker, targetStats, distance / 20, myStats.damage.GetValue()));
    }
}


