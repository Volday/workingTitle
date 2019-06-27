using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectGenerator : MonoBehaviour {

    public GameObject cube;

    public GameObject tree;

    public GameObject waterSurface;
    public GameObject waterNavMeshBottom;
    public GameObject waterSurfaceBottom;

    public GameObject invisibleWall;

    public float waterHeightShift;

    private float meshHeightMultiplier;
    private MapGenerator mapGenerator;

    void Start () {
        mapGenerator = GetComponent<MapGenerator>();
        meshHeightMultiplier = mapGenerator.meshHeightMultiplier;
	}

    public void OneBuildGenerate(Vector2 location, Vector2 dimension, float angle, int seed, int mapWidth, int mapHeight) {
        GameObject newCube = Instantiate(cube, new Vector3(mapWidth - location.x - (float)MapGenerator.mapChunkSize / 2, 7, mapHeight - location.y - (float)MapGenerator.mapChunkSize / 2), Quaternion.identity, transform);
        newCube.transform.Rotate(0, -angle + 90, 0);
        newCube.transform.localScale = new Vector3(dimension.x, 5, dimension.y);
    }

    public void TreeGenerate(Vector2 location, int seed, int mapWidth, int mapHeight, float height)
    {
        GameObject newTree = Instantiate(tree, new Vector3(mapWidth - location.x - (float)MapGenerator.mapChunkSize / 2, height, mapHeight - location.y - (float)MapGenerator.mapChunkSize / 2), Quaternion.identity, transform);
    }

    public void WaterGenerate(Vector2 location, int xScale, int zScale, float height)
    {
        float chunkSize = (float)MapGenerator.mapChunkSize - 1;

        GameObject newWaterSurface = Instantiate(waterSurface, new Vector3(location.x, height + waterHeightShift, location.y), Quaternion.identity, transform);
        newWaterSurface.transform.localScale = new Vector3(xScale, 1, zScale);

        GameObject newWaterNavMeshBottom = Instantiate(waterNavMeshBottom, new Vector3(location.x, waterHeightShift - 1, location.y), Quaternion.identity, transform);
        newWaterNavMeshBottom.transform.localScale = new Vector3(xScale * chunkSize, height * 2, zScale * chunkSize);

        for (int x = 0; x < xScale; x++) {
            for (int y = 0; y < zScale; y++) {
                if (x == 0 || y == 0 || x == xScale - 1 || y == zScale - 1) {
                    GameObject newWaterSurfaceBottom = Instantiate(waterSurfaceBottom, new Vector3(x * chunkSize - chunkSize, 0, 
                        y * chunkSize - chunkSize), Quaternion.identity, transform);
                    newWaterSurfaceBottom.transform.localScale = new Vector3(chunkSize, 1, chunkSize);
                    if (mapGenerator.regions.Length > 0) {
                        newWaterSurfaceBottom.GetComponentInChildren<Renderer>().material.SetColor("_Color", mapGenerator.regions[0].colour);
                    }
                }
            }
        }
    }

    public void InvisibleWallGenerator(List<Vector2> invisibleWallsPoints, int mapWidth, int mapHeight) {
        GameObject invisibleWalls = new GameObject();
        invisibleWalls.transform.parent = transform;
        invisibleWalls.name = "Invisible Walls";
        for (int t = 0; t < invisibleWallsPoints.Count; t++) {
            GameObject newTree = Instantiate(invisibleWall, new Vector3(mapWidth - invisibleWallsPoints[t].x - (float)MapGenerator.mapChunkSize / 2, 0, mapHeight - invisibleWallsPoints[t].y - (float)MapGenerator.mapChunkSize / 2), Quaternion.identity, invisibleWalls.transform);
        }
    }
}
