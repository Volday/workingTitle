using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocationGenerator : MonoBehaviour {

    public NavMeshSurface surface;

	void Start () {
        LandscapeGeneratorType1 landscapeGeneratorType1 = FindObjectOfType<LandscapeGeneratorType1>();
        landscapeGeneratorType1.LandscapeGenerate();
    }

    public void GenerationComplete() {
        surface.BuildNavMesh();
    }

    public void AllocationGameObdjects(int[,] coverageMap) {

    }
}
