using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class PlayerMove : MonoBehaviour
{
    protected NavMeshAgent playerNavAgent;
    protected PhotonView playerPhotonView;
    protected Animator playerAnimator;
    [SerializeField]
    protected Text nomeText;
    [SerializeField]
    protected GameObject canvasJogador;
   
    public virtual void Move()
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

    }
    public void ColorName()
    {
        if (playerPhotonView.IsMine)
        {
            nomeText.color = Color.red;
        }

    }
}
