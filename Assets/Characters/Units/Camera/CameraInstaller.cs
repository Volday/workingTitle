using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInstaller : MonoBehaviour
{
    public GameObject cameraTarget;

    void Start()
    {
        cameraTarget.GetComponent<CameraTargetMove>().trackedObject = gameObject;
        Instantiate(cameraTarget, transform.position, Quaternion.identity);
        Destroy(this);
    }
}
