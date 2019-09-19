using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour
{

    private static CombatTextManager instance;

    public GameObject textPrefab;

    public RectTransform CanvasTransform;

    public float fadeTime;

    public float speed;

    public Vector3 direction;

    public static CombatTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }

            return instance;
        }

    }
    public void CreatText(Vector3 position, string text, Color color, FontStyle font, int size)
    {
        GameObject sct = Instantiate(textPrefab, position, Quaternion.identity);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;
        sct.GetComponent<Text>().fontStyle = font;
        sct.GetComponent<Text>().fontSize = size;
        sct.transform.SetParent(CanvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        sct.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
        sct.GetComponent<RectTransform>().localPosition += new Vector3(0.9f, 40, -100);
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime);



    }
     public void CriticText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, "*" + dano.ToString() + "*", new Color(0.7607844f, 0.7607844f, 0.7607844f), FontStyle.Bold, 15);
    }
    public void AttackText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, dano.ToString(), new Color(0.7607844f, 0.7607844f, 0.7607844f), FontStyle.Bold, 13);
    }
    public void MyCriticText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, "*" + dano.ToString() + "*", new Color(1f, 0, 0), FontStyle.Bold, 15);
    }
    public void MyAttackText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, dano.ToString(), new Color(1f, 0, 0), FontStyle.Bold, 13);
    }
    public void MissText(Transform pos)
    {
        Instance.CreatText(pos.position, "Miss", new Color(1f, 1f, 1f, 0.8f), FontStyle.Normal, 13);
    }
    public void Exp(Transform pos, int experiencie)
    {
        Instance.CreatText(pos.position,"+" + experiencie.ToString() + " Experience", Color.white , FontStyle.Bold, 15);
    }
}
