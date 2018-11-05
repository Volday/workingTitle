using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationGenerator : MonoBehaviour {

	void Start () {
        LandscapeGeneratorType1 landscapeGeneratorType1 = FindObjectOfType<LandscapeGeneratorType1>();
        landscapeGeneratorType1.LandscapeGenerate();
    }

    public void AllocationGameObdjects(int[,] coverageMap) {

    }
}
