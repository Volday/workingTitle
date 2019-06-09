using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public List<GameObject> target = new List<GameObject>();
    public int currentSlide = 4;
    public GameObject spawn;

    private void Start()
    {
        SwitchNow();
    }

    void Update()
    {
        SwitchTriger();
        SwitchNow();
    }

    void SwitchNow() {
        GetComponent<CameraTargetMove>().trackedObject = target[currentSlide];
    }

    void SwitchTriger() {
        if (Input.GetAxis("1") > 0) {
            currentSlide = 1;
        }
        if (Input.GetAxis("2") > 0)
        {
            currentSlide = 2;
        }
        if (Input.GetAxis("3") > 0)
        {
            currentSlide = 3;
        }
        if (Input.GetAxis("4") > 0)
        {
            currentSlide = 4;
        }
        if (Input.GetAxis("5") > 0)
        {
            currentSlide = 5;
        }
        if (Input.GetAxis("6") > 0)
        {
            currentSlide = 6;
        }
        if (Input.GetAxis("7") > 0)
        {
            currentSlide = 7;
        }
        if (Input.GetAxis("8") > 0)
        {
            currentSlide = 8;
        }
        if (Input.GetAxis("9") > 0)
        {
            currentSlide = 9;
        }
        if (Input.GetAxis("0") > 0)
        {
            currentSlide = 0;
        }
        //if (Input.GetAxis("l") > 0)
        //{
        //    spawn.SetActive(true);
        //    Debug.Log("l");
        //}
    }
}
