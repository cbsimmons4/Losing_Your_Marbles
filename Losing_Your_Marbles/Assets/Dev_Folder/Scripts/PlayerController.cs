using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject FreezeGun;

    // Start is called before the first frame update
    void Start()
    {
        FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (0.6f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + (0.6f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (0.6f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
        }

    }
}
