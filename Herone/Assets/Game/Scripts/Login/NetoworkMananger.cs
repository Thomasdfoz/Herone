using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetoworkMananger : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject painelL, painelS;
    [SerializeField]
    private InputField nomeJogador, nomeSala;
    [SerializeField]
    private Text txtNick;
    [SerializeField]
    private GameObject[] jogador;
    [SerializeField]
    private GameObject canvas;


    private int id;

    // Start is called before the first frame update
    void Start()
    {

        painelL.SetActive(true);
        painelS.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void Login()
    {
        nomeJogador.text = char.ToUpper(nomeJogador.text[0]) + nomeJogador.text.Substring(1);
        PhotonNetwork.NickName = nomeJogador.text;
        PhotonNetwork.ConnectUsingSettings();
        painelL.SetActive(false);
        painelS.SetActive(true);
    }
    public void CriaSala()
    {
        PhotonNetwork.JoinOrCreateRoom(nomeSala.text, new RoomOptions(), TypedLobby.Default);   
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        txtNick.text = PhotonNetwork.NickName;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
       
    }
    public override void OnJoinedLobby()
    {
       
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
                
    }
    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.CurrentRoom.Name);
        print(PhotonNetwork.CurrentRoom.PlayerCount);
        print(PhotonNetwork.NickName);
       

        PhotonNetwork.Instantiate(jogador[id].name, new Vector3(0, Random.Range(1, 8), 0), Quaternion.Euler(45, 45, 45), 0);
        canvas.SetActive(false);

    }
    public void SetID(int Id)
    {
        id = Id;
    }
}
