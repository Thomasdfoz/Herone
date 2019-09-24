using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    Manager manager;
    private string senhaMd5;
    public InputField inputEmail;
    public InputField inputSenha;
    public Text textReturn;

    private void Start()
    {
        manager = GetComponent<Manager>();
    }
    // Start is called before the first frame update    
    public void LoginBtn()
    {
        EncryptMd5(inputSenha.text);
    }

    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();

        form.AddField("action", "login");
        form.AddField("Email", inputEmail.text);
        form.AddField("Senha", senhaMd5);

        WWW retorno = new WWW("https://herone.000webhostapp.com/teste/Login.php", form);

        yield return retorno;

        if (retorno.error == null)
        {
            string r = retorno.text;
            textReturn.text = r;
            Debug.Log(r);
            if (r == "LOGADO!!!")
            {
                PlayerPrefs.SetString("emailPF", inputEmail.text);
                PlayerPrefs.SetString("senhaPF", senhaMd5);
                manager.Logado();
            }
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
        StartCoroutine(LoginUser());
    }


}

