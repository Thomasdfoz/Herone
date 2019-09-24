using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Registro : MonoBehaviour
{
    private string senhaMd5;
    public InputField inputEmail;
    public InputField inputSenha;
    public Text textReturn;

    // Start is called before the first frame update    
    public void RegistroBtn()
    {
        EncryptMd5(inputSenha.text);
    }

    IEnumerator Cadastro()
    {
        WWWForm form = new WWWForm();

        form.AddField("action", "registro");
        form.AddField("Email", inputEmail.text);
        form.AddField("Senha", senhaMd5);

        WWW retorno = new WWW("https://herone.000webhostapp.com/teste/Login.php", form);

        yield return retorno;

        if (retorno.error == null)
        {
            string r = retorno.text;
            textReturn.text = r;
            Debug.Log(r);
        }
        else
        {
            Debug.Log("error " + retorno.error);
        }
    }

    public void EncryptMd5(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] data = md5.ComputeHash(System.Text.ASCIIEncoding.Default.GetBytes(input));
        System.Text.StringBuilder sbString = new System.Text.StringBuilder();
        for (int i = 0; i < data.Length; i++)
            sbString.Append(data[i].ToString("x2"));
        senhaMd5 = sbString.ToString();
        StartCoroutine(Cadastro());
    }


}