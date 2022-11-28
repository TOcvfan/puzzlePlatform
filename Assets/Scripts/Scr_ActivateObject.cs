using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ActivateObject : MonoBehaviour
{

    [SerializeField]
    private bool blinking, blinkOffset;
    private bool activated;

    private Renderer rend;
    // Start is called before the first frame update

    [SerializeField]
    private ActivatedObjectType activatedObjectType;

    [SerializeField]
    private GameObject objectToActivate;

    private enum ActivatedObjectType { 
    
        door,
        movingPlatform,
        appearingPlatform
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        InvokeRepeating("Blinking", 0, .5f);
        activated = false;
    }


    void Blinking()
    {
        if (blinking)
            if (blinkOffset)
            {
                rend.material.SetTextureOffset("_MainTex", new Vector2(0, .5f));
                blinkOffset = false;
            }
            else
            {
                rend.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
                blinkOffset = true;
            }
    }

    void Activation() {
        print("Activated");
        if (activatedObjectType == ActivatedObjectType.door)
        {
            if (!activated)
            {
                objectToActivate.GetComponent<Animator>().SetTrigger("Open");
                activated = true;
            }
        }

    }
}
