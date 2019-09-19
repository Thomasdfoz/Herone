using UnityEngine;
using System.Collections;
using Assets.MyGame.Scripts;


public class Machado : Weapon
{   
    private void Start()
    {       
        Debug.Log(ToString());

    }
    public override string ToString()
    {
        return "Descrição: " +
            nome + " || Tipo: " +
            type +
            " || Dano: " + dano;
    }

}

