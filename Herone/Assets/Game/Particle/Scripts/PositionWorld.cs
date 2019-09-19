using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionWorld : MonoBehaviour
{
    public Transform focus;
    public GameObject player;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        focus = player.GetComponent<PlayerController>().focus.transform;
    }
    void Update()
    {
        if (focus !=null)
        {
            transform.position = focus.position;
            if (focus.tag == "Object")
            {
                transform.position = new Vector3(transform.position.x, +1, transform.position.z);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
