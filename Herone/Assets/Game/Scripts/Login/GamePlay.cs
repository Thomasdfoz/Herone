using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public Text textoRetorno;
    public Text textoDADOPlayer;
    public Text textoDADOAI;

    public int pontosPlayer;
    public int pontosAi;
    public int resultado;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void inicio()
    {
        int i = Random.Range(1, 21);
        pontosAi = i;
        textoDADOAI.text = i.ToString();
    }
    public void PlayerDado()
    {
        int i = Random.Range(1, 21);
        pontosPlayer = i;
        textoDADOPlayer.text = i.ToString();

        if (pontosPlayer > pontosAi)
        {
            resultado = 1;
            StartCoroutine(EnviaResultado());
            
        }
        else if (pontosPlayer == pontosAi)
        {
            resultado = 0;
            textoRetorno.text = "Player Empatou!!";
            StartCoroutine(EnviaResultado());
            
        }
        else
        {
            resultado = -1;
            StartCoroutine(EnviaResultado());
        }
    }

    IEnumerator EnviaResultado()
    {
        WWWForm form = new WWWForm();

        form.AddField("action", "resultado");
        form.AddField("Resultado", resultado.ToString());
        form.AddField("Email", PlayerPrefs.GetString("emailPF"));        
        form.AddField("Senha", PlayerPrefs.GetString("senhaPF"));



        WWW retorno = new WWW("https://herone.000webhostapp.com/teste/GamePlay.php", form);

        yield return retorno;

        if (retorno.error == null)
        {
            string r = retorno.text;
            textoRetorno.text = r;
            Debug.Log(r);
        }
        else
        {
            Debug.Log("error " + retorno.error);
        }
    }
}

