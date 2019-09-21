using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of enemy stats, loosing health and dying. */

public class EnemyStats : CharacterStats {
    public int experiencie; 
   
    public override void Die(GameObject killer)
	{
		base.Die(killer);
        killer.GetComponent<PlayerStats>().CurrentExperience += experiencie;
        CombatTextManager.Instance.Exp(killer.transform, experiencie);

		// Add ragdoll effect / death animation
		Destroy(gameObject);
	}
    public override void TakeDamage(GameObject attacker, float damage, InfAtk inf, TypeDamage typeDamage)
    {
       
        int realDamage = 0;
        if (!die)
        {
            // Subtract the armor value
            if (typeDamage == TypeDamage.physic)
            {
                // Subtract the armor value
                // formula do lol 100/100+armadura e o dano é dividio por essa formula, assim se eu tiver 50 de armadura vou receber 2/3 de dano
                realDamage = (int)(damage * DefeseValue(armor));
                realDamage = Mathf.Clamp(realDamage, 0, int.MaxValue);
            }
            else
            {
                realDamage = (int)damage;
            }           

            // Damage the character
            health.CurrentVal -= realDamage;
            if (realDamage > 0)
            {
                if (inf == InfAtk.normal)
                {
                    //CombatText.print
                    CombatTextManager.Instance.AttackText(transform, realDamage);
                }
                else if (inf == InfAtk.critic)
                {
                    CombatTextManager.Instance.CriticText(transform, realDamage);
                }
            }
            else
            {
                CombatTextManager.Instance.MissText(transform);
            }




            // If health reaches zero
            if (health.CurrentVal <= 0)
            {
                Die(attacker);
            }
        }

    }
   /*public void BurningDamage(GameObject attacker, float damage, TypeDamage typeDamage, float cowndown)
    {
        activeTemp = true;
        if (temp >= cowndown)
        {
            TakeDamage(attacker, damage, InfAtk.normal, typeDamage);
            temp = 0;
        }

    }*/
}
