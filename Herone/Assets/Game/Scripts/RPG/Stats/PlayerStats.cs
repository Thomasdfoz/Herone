using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/* Handles the players stats and adds/removes modifiers when equipping items. */

public class PlayerStats : CharacterStats
{
    [Space]
    [Header("LEVEL", order = 1)]
    public double level;
    public double currentExperience;
    public double expNextLevel;
    public Text danoText;
    public Text currentExpText;
    public Text expNextLevelText;
    public Text levelText;

    // Use this for initialization
    void Start()
    {

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }
    private void LateUpdate()
    {

        if (!ranged)
        {
            rangeAttack = 1.6f;
        }

        UpLevel();
        danoText.text = damage.GetValue().ToString();
        currentExpText.text = currentExperience.ToString();
        expNextLevelText.text = expNextLevel.ToString();
        levelText.text = "Level " + level.ToString();
    }

    // Called when an item gets equipped/unequipped
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        // Add new modifiers
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        // Remove old modifiers
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

    }

    public override void Die(GameObject killer)
    {
        // base.Die(killer);
        // PlayerManager.instance.KillPlayer();
        health.CurrentVal = health.MaxVal;
        Debug.Log(currentExperience + " " + ExpNextLevel(level - 1));
        while (currentExperience < ExpNextLevel(level - 1) && level >= 2)
        {
            level--;
        }     
               
    }
    public override void TakeDamage(GameObject attacker, float damage, InfAtk inf, TypeDamage typeDamage)
    {
        int realDamage = 0;
        if (!die)
        {
            // Subtract the armor value
            if (typeDamage == TypeDamage.physic)
            {
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
                    CombatTextManager.Instance.MyAttackText(transform, realDamage);
                }
                else if (inf == InfAtk.critic)
                {
                    CombatTextManager.Instance.MyCriticText(transform, realDamage);
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
    private void UpLevel()
    {
        expNextLevel = Mathf.Round((float)ExpNextLevel(level));
        if (currentExperience >= expNextLevel)
        {
            level++;
            health.MaxVal += 100;
            moveSpeed.AddModifier(1);
            damage.AddModifier(50);
            health.CurrentVal = health.MaxVal;
            UpdateMoveSpeed();
            expNextLevel = Mathf.Round((float)ExpNextLevel(level));
        }

    }
    private double ExpNextLevel(double lvl)
    {
        if (lvl <= 1)
            return 100;       
        else        
            return (3 * (10 * lvl * lvl)) + ExpNextLevel(lvl - 1);        
    }

}
