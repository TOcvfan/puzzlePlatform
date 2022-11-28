using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCombat : MonoBehaviour
{

    private int health;
    
    [SerializeField]
    private bool gotGun, canShoot;

    private Ray castRay;
    private RaycastHit hitObject;

    [SerializeField]
    private Transform Camera;

    private float shootingDelay;

    [SerializeField]
    private GameObject sparks;

    public int Health {
        get { return health; }
        set { health += value;
            if (health <= 0)
            {
                //GameOver
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.Find finds a child of the object this is script is on, defined by the name given.
        Camera = transform.Find("Main Camera");
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (gotGun)
        {
            if (Input.GetButton("Fire1"))
            {
                if (canShoot)
                {
                    castRay.origin = Camera.position;
                    Debug.DrawRay(castRay.origin, Camera.forward * 10, Color.red, 2);

                    if (Physics.Raycast(castRay.origin, Camera.forward, out hitObject))
                    {
                        
                        if (hitObject.transform.tag == "Enemy")
                        {
                            print("Hit Enemy");
                        }
                        else if (hitObject.transform.tag == "Target")
                        {
                            hitObject.transform.gameObject.SendMessage("Activation");
                        }

                        Instantiate(sparks, hitObject.point, Quaternion.LookRotation(hitObject.normal));
                    }
                    canShoot = false;

                    StartCoroutine(shotTimer());
                }
                
            }
        }
    }

    IEnumerator shotTimer() {

        yield return new WaitForSeconds(.1f);

        canShoot = true;

        StopCoroutine(shotTimer());
    }

}
