using UnityEngine;
namespace Assets.MyGame.Scripts
{
    static class CharacterManager
    {
        public static int MaxDamage(int danoArma, int skill)
        {
            return Mathf.RoundToInt(((danoArma * 2) * (skill) / 10));
        }
        public static int Damage(float maxDamage)
        {
            return Mathf.RoundToInt(Random.Range(maxDamage * 0.2f, maxDamage));
        }
        public static int Critical(int maxDamage, int additionalCriticalDamage)
        {
            return Mathf.RoundToInt(Random.Range(maxDamage, maxDamage * (additionalCriticalDamage / 100)));
        }
        public static int CheckSkill(Weapon arma, int meleeSkill, int rangedSkill, int magicSkill)
        {
            if (arma.type == EnumWaeponType.Distance)
                return rangedSkill;
            if (arma.type == EnumWaeponType.Magic)
                return magicSkill;
            return meleeSkill;
        }
        public static bool RandomBool(float chance, float max)
        {
            float percentage = (chance / max) * 100;
            int i = Random.Range(1, 101);
            
            if (i < percentage)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void RotateTowards(Transform pos1, Transform pos2)
        {
            Vector3 direction = (pos1.position - pos2.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            pos2.rotation = Quaternion.Slerp(pos2.rotation, lookRotation, 0.08f);
        }
    }
}
