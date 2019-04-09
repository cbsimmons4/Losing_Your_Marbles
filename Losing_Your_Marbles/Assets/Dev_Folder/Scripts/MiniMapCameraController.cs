using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = this.transform.parent.position + new Vector3(1000, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       this.transform.position = this.transform.parent.position + new Vector3(1000, 0, 0);
    }
}
