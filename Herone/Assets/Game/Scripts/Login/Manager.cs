using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject canvasLogin;
    public GameObject canvasRegisto;
    public GameObject canvasGame;
    public GameObject canvasStats;

    void Start()
    {
        canvasLogin.SetActive(true);
        canvasRegisto.SetActive(false);
      
    }
    public void ResgistrarMenu()
    {
       
        canvasLogin.SetActive(false);
        canvasRegisto.SetActive(true);
        
    }
    public void LoginMenu()
    {
       
        canvasLogin.SetActive(true);
        canvasRegisto.SetActive(false);
        
    }
    public void Logado()
    {
       
        canvasLogin.SetActive(false);
        canvasRegisto.SetActive(false);
        
    }   
}
