using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheseCursor : MonoBehaviour
{
    Camera cam;

    public GameObject owner; 

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (owner != null && owner.GetComponent<Player>().isActiveAndEnabled) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 target = hit.point;
                target.y = transform.position.y;

                transform.position = target;
            }
        }
    }
}
