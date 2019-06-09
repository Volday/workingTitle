using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMove : MonoBehaviour
{
    //Передаётся из объекта слежения
    public GameObject trackedObject;

    public Camera camera;
    public LayerMask rayCastLayers;
    Ray ray;
    RaycastHit hit;
    public bool betweenMouseAndTarget = true;
    public float shiftCoeficient = 0;

    void Start()
    {
        camera.GetComponent<CameraMove>().target = gameObject;
        Instantiate(camera, transform.position, Quaternion.identity);
    }

    void Update() {
        if (betweenMouseAndTarget)
        {
            Vector3 shift = new Vector3(Input.mousePosition.x - Screen.width / 2, 0, Input.mousePosition.y - Screen.height / 2);
            transform.position = trackedObject.transform.position + shift * shiftCoeficient;
        }
        else {
            transform.position = trackedObject.transform.position;
        }
    }
}
