using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour
{
    public bool useScreenEdgeInput;
    public float screenEdgeBorder = 50f;
    public float screenEdgeMovementSpeed = 3f;
    private Transform m_Transform;
    private CinemachineVirtualCamera vCam;
    public bool activeFallow;
    public Text txt;


    // Start is called before the first frame update
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (useScreenEdgeInput)
        {
            Vector3 desiredMove = new Vector3();

            Rect leftRect = new Rect(0, 0, screenEdgeBorder, Screen.height);
            Rect rightRect = new Rect(Screen.width - screenEdgeBorder, 0, screenEdgeBorder, Screen.height);
            Rect upRect = new Rect(0, Screen.height - screenEdgeBorder, Screen.width, screenEdgeBorder);
            Rect downRect = new Rect(0, 0, Screen.width, screenEdgeBorder);

            desiredMove.x = leftRect.Contains(MouseInput) ? -1 : rightRect.Contains(MouseInput) ? 1 : 0;
            desiredMove.z = upRect.Contains(MouseInput) ? 1 : downRect.Contains(MouseInput) ? -1 : 0;

            desiredMove *= screenEdgeMovementSpeed;
            desiredMove *= Time.deltaTime;
            desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
            desiredMove = m_Transform.InverseTransformDirection(desiredMove);

            m_Transform.Translate(desiredMove, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeFallow = !activeFallow;  
        }
        FollowPlayer(activeFallow);

        if (!activeFallow)
        {
            txt.text = "Camera Destravada!!!!";
        }
        else
        {
            txt.text = "Camera travada!!!!";
        }
    }

    private Vector2 MouseInput
    {
        get { return Input.mousePosition; }
    }
    public void FollowPlayer(bool active)
    {
        if (active)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            vCam.Follow = player.transform;
        }
        else
        {
            vCam.Follow = null;
        }
      

    }
}
