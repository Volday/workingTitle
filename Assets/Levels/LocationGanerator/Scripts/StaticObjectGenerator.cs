using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectGenerator : MonoBehaviour {

    public GameObject cube;
    public GameObject tree;

    private float meshHeightMultiplier;
    MapGenerator mapGenerator;

    // Use this for initialization
    void Start () {
        MapGenerator mapGenerator = GetComponent<MapGenerator>();
        meshHeightMultiplier = mapGenerator.meshHeightMultiplier;
	}

    public void OneBuildGenerate(Vector2 location, Vector2 dimension, float angle, int seed, int mapWidth, int mapHeight) {
        GameObject newCube = Instantiate(cube, new Vector3(mapWidth - location.x - (float)MapGenerator.mapChunkSize / 2, 7, mapHeight - location.y - (float)MapGenerator.mapChunkSize / 2), Quaternion.identity, transform);
        newCube.transform.Rotate(0, -angle + 90, 0);
        newCube.transform.localScale = new Vector3(dimension.x, 5, dimension.y);
    }

    public void TreeGenerate(Vector2 location, int seed, int mapWidth, int mapHeight, float height)
    {
        GameObject newCube = Instantiate(tree, new Vector3(mapWidth - location.x - (float)MapGenerator.mapChunkSize / 2, height, mapHeight - location.y - (float)MapGenerator.mapChunkSize / 2), Quaternion.identity, transform);
        //newCube.transform.Rotate(0, 0, 0);
        //newCube.transform.localScale = new Vector3(1, 1, 1);
    }
}
