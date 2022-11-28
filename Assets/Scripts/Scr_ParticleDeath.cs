using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ParticleDeath : MonoBehaviour
{

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.timeSinceLevelLoad + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > timer)
        {
            Destroy(this.gameObject);
        }
    }
}
