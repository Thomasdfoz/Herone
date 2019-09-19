using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBar : MonoBehaviour
{
    GameObject cam;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        
    }
}
