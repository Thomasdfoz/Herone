using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public EnumWaeponType type;
    public EnumWaeponHands hands;
    public string nome;
    public int dano;
   


    public override string ToString()
    {
        return "Descrição: " +
            nome +
            ", Dano: " +
            dano;
    }
}

