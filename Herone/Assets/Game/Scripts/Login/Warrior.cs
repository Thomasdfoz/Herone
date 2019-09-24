using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Warrior : PlayerMove
{

    void Start()
    {
        playerNavAgent = GetComponent<NavMeshAgent>();
        playerPhotonView = GetComponent<PhotonView>();
        playerAnimator = GetComponent<Animator>();
        nomeText.text = playerPhotonView.Owner.NickName;
    }

    void Update()
    {
        ColorName();
        canvasJogador.transform.LookAt(Camera.main.transform);
        if (playerPhotonView.IsMine)
        {
            Move();
        }
    }
    public override void Move()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {

                playerNavAgent.destination = hit.point;

            }
        }

        if(playerNavAgent.remainingDistance <= playerNavAgent.stoppingDistance)
        {
            playerAnimator.SetBool("Walk", false);
        }
        else
        {
            playerAnimator.SetBool("Walk", true);

        }
    }
}
