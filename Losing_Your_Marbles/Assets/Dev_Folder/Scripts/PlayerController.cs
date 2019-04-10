using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject FreezeGun;
    public GameObject OpenCH;
    public GameObject ClosedCH;

    // Start is called before the first frame update
    void Start()
    {
        FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
        this.OpenCH.SetActive(true);
        this.ClosedCH.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
            this.OpenCH.SetActive(false);
            this.ClosedCH.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
            this.OpenCH.SetActive(true);
            this.ClosedCH.SetActive(false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            FreezeGun.transform.position = FreezeGun.transform.parent.position + FreezeGun.transform.parent.right + (1.3f * FreezeGun.transform.parent.forward) + (-0.6f * FreezeGun.transform.parent.up);
            this.OpenCH.SetActive(true);
            this.ClosedCH.SetActive(false);
        }

    }
}
