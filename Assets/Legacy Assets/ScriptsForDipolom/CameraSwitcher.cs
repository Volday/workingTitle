using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Slide[] slides;
    public int currentSlide = 0;
    public GameObject spawn;
    private bool hasImput = false;
    float nextImputTime = 0.5f;
    float currentNextImputTime = 0;

    private void Start()
    {
        SwitchNow();
    }

    void Update()
    {
        if (hasImput == true) {
            currentNextImputTime += Time.deltaTime;
            if (currentNextImputTime > nextImputTime) {
                currentNextImputTime = 0;
                hasImput = false;
            }
        }
        SwitchTriger();
        SwitchNow();
    }

    void SwitchNow() {
        for (int t = 0; t < slides.Length; t++) {
            if (slides[t].slideImage != null) {
                slides[t].slideImage.SetActive(false);
            }
        }
        if (slides[currentSlide].slidePisition != null) {
            GetComponent<CameraTargetMove>().trackedObject = slides[currentSlide].slidePisition;
        }
        if (slides[currentSlide].slideImage != null) {
            slides[currentSlide].slideImage.SetActive(true);
        }
    }

    void SwitchTriger() {
        if (Input.GetAxis("1") > 0)
        {
            spawn.SetActive(true);
        }
        if (Input.GetAxis("Horizontal") > 0 && !hasImput) {
            hasImput = true;
            if (slides.Length > currentSlide + 1)
            {
                currentSlide++;
            }
            else {
                currentSlide = 0;
            }
        }
        if (Input.GetAxis("Horizontal") < 0 && !hasImput)
        {
            hasImput = true;
            if (currentSlide > 0)
            {
                currentSlide--;
            }
            else
            {
                currentSlide = slides.Length - 1;
            }
        }
    }
}

[System.Serializable]
public class Slide {
    public int index;
    public GameObject slidePisition;
    public GameObject slideImage;
}
