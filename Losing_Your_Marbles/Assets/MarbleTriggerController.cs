using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleTriggerController : MonoBehaviour
{

    public GameObject mainCamera;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = mainCamera.transform.position;
    }
}
