using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerInteract : MonoBehaviour
{

    private Ray rayOrigin;
    private RaycastHit objectHit;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(rayOrigin.origin, cameraTransform.forward, out objectHit))
        {

        }
    }
}
