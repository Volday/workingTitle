using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectGenerator : MonoBehaviour {

    public GameObject cube;

	// Use this for initialization
	void Start () {
	}

    public void OneBuildGenerate(Vector2 location, Vector2 dimension, float angle, int seed, int mapWidth, int mapHeight) {
        GameObject newCube = Instantiate(cube, new Vector3(mapWidth - location.x - (float)MapGenerator.mapChunkSize / 2, 7, mapHeight - location.y - (float)MapGenerator.mapChunkSize / 2), Quaternion.identity, transform);
        newCube.transform.Rotate(0, -angle + 90, 0);
        newCube.transform.localScale = new Vector3(dimension.x, 5, dimension.y);
    }
}
