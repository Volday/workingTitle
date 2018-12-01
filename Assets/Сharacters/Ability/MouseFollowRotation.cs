using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    private Vector3 mousePosition;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Vector3 target = hit.point;
            target.y = transform.position.y;

            transform.LookAt(target);
        }
    }
}