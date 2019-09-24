using System;

[Serializable]
public class player
{
    public string username;


    public int idPlayer;


    public string password;

    

    public player() { }

    public player(string name, int id, string pass)
    {
        username = name;


        password = pass;

       
    }
}
