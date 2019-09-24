using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Cube : PlayerMove
{
    void Start()
    {
        
        playerNavAgent = GetComponent<NavMeshAgent>();
        playerPhotonView = GetComponent<PhotonView>();
        playerAnimator = GetComponent<Animator>();
        nomeText.text = playerPhotonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        ColorName();
        canvasJogador.transform.LookAt(Camera.main.transform);
        if (playerPhotonView.IsMine)
        {
            Move();
        }
    }
}
