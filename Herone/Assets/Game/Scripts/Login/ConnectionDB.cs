using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionDB : MonoBehaviour
{
    public Text[] rankText;
    public Text TextoNome;
    public Text textoVitorias;
    public Text textoDerrotas;

    private string[] pegaStats;
    private string[] rankSplit;


    void Start()
    {

    }
    public void Atualizar()
    {
        StartCoroutine(EnviaResultado());
        StartCoroutine(Rank());
    }
    IEnumerator EnviaResultado()
    {
        WWWForm form = new WWWForm();

        form.AddField("action", "stats");
        form.AddField("Email", PlayerPrefs.GetString("emailPF"));
        form.AddField("Senha", PlayerPrefs.GetString("senhaPF"));



        WWW retorno = new WWW("https://herone.000webhostapp.com/teste/GamePlay.php", form);

        yield return retorno;

        if (retorno.error == null)
        {
            string r = retorno.text;
            Debug.Log(r);
            pegaStats = r.Split('|');
            Debug.Log(pegaStats[1]);
            Debug.Log(pegaStats[2]);

            TextoNome.text = "Player : " + PlayerPrefs.GetString("emailPF");

            textoVitorias.text = "Vitorias : " + pegaStats[1];
            textoDerrotas.text = "Derrotas : " + pegaStats[2];


        }
        else
        {
            Debug.Log("error " + retorno.error);
        }
    }
    IEnumerator Rank()
    {
        WWWForm form = new WWWForm();

        form.AddField("action", "rank");

        WWW retorno = new WWW("https://herone.000webhostapp.com/teste/GamePlay.php", form);

        yield return retorno;

        if (retorno.error == null)
        {
            string r = retorno.text;
            Debug.Log(r);
            rankSplit = r.Split('|');
            for (int i = 0; i < 10; i++)
            {
                rankText[i].text = rankSplit[i];
            }
        }
        else
        {
            Debug.Log("error " + retorno.error);
        }
    }
}
