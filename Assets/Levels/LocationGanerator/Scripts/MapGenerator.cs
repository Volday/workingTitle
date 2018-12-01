using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode { NoiseMap, ColourMap, Mesh, FalloffMap };
    public DrawMode drawMode;

    public Noise.NormalizeMode normalizeMode;

    public const int mapChunkSize = 97;

    private int widthLocation;
    private int heightLocation;

    [Range(0, 4)]
    public int editorPreviewLOD;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;
    public bool useIslands;
    [Range(1, 0)]
    public float immersionDepth;
    [Range(0.1f, 1)]
    public float coastalSlope;
    public float islandsNoiseScale;

    public bool useFlatShading;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] falloffMapContain;
    float[,] falloffMap;
    float[,] falloffMapEdge;
    float[,] falloffMapOuterCorner;
    float[,] falloffMapExceptEdge;
    float[,] falloffMapDoubleEdge;

    [HideInInspector] public float[,] islandsMap;

    Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

    public void Awake()
    {
        // 0 Вокруг, 1 с одной стороны, 2 с двух сторон(внешний угол), 3 кроме стороны, 4 с двух сторон
        falloffMapContain = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 0);
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 0);
        falloffMapEdge = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 1);
        falloffMapOuterCorner = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 2);
        falloffMapExceptEdge = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 3);
        falloffMapDoubleEdge = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 4);
    }

    public void DrawMapInEditor() {
        MapData mapData = GenerateMapData(Vector2.zero, 0, new Vector2(0, 0));
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorPreviewLOD, useFlatShading), TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
        else if(drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(falloffMap));
        }
    }

    public void RequestMapData(Vector2 center, Action<MapData> callback, int falloffAngle, Vector2 coord) {
        ThreadStart threadStart = delegate
        {
            MapDataThread(center, callback, falloffAngle, coord);
        };

        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 center, Action<MapData> callback, int falloffAngle, Vector2 coord) {
        MapData mapData = GenerateMapData(center, falloffAngle, coord);
        lock (mapDataThreadInfoQueue)
        {
            mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback) {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(mapData , lod, callback);
        };

        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback) {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod, useFlatShading);
        lock (meshDataThreadInfoQueue) {
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }

    void Update()
    {
        if (mapDataThreadInfoQueue.Count > 0) {
            for (int i = 0; i < mapDataThreadInfoQueue.Count; i++ ) {
                MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }

        if (meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    MapData GenerateMapData(Vector2 center, int falloffAngle, Vector2 coord) {

        float[,] noiseMap = Noise.GenerateNoiseMap(seed, center + offset, mapChunkSize + 2, mapChunkSize + 2, noiseScale, octaves, persistance, lacunarity, normalizeMode);

        if (useIslands)
        {
            for (int y = 0; y < (mapChunkSize + 2); y++)
            {
                for (int x = 0; x < (mapChunkSize + 2); x++)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - Mathf.Clamp01(islandsMap[mapChunkSize + 1 - x + (widthLocation - 1 - (int)coord.x) * (mapChunkSize - 1), y + (heightLocation - 1 - (int)coord.y) * (mapChunkSize - 1)]));
                }
            }
        }

        if (useFalloff) {
            for (int y = 0; y < (mapChunkSize + 2); y++)
            {
                for (int x = 0; x < (mapChunkSize + 2); x++)
                {
                    if (falloffAngle == 44)
                    {
                        falloffMapContain[x, y] = falloffMapOuterCorner[mapChunkSize + 1 - y, x];
                    }
                    if (falloffAngle == 33)
                    {
                        falloffMapContain[x, y] = falloffMapOuterCorner[mapChunkSize + 1 - x, y];
                    }
                    if (falloffAngle == 22)
                    {
                        falloffMapContain[x, y] = falloffMapOuterCorner[mapChunkSize + 1 - x, mapChunkSize + 1 - y];
                    }
                    if (falloffAngle == 11)
                    {
                        falloffMapContain[x, y] = falloffMapOuterCorner[x, y];
                    }


                    if (falloffAngle == 4)
                    {
                        falloffMapContain[x, y] = falloffMapEdge[mapChunkSize + 1 - y, x];
                    }
                    if (falloffAngle == 3)
                    {
                        falloffMapContain[x, y] = falloffMapEdge[mapChunkSize + 1 - x, y];
                    }
                    if (falloffAngle == 2)
                    {
                        falloffMapContain[x, y] = falloffMapEdge[y, x];
                    }
                    if (falloffAngle == 1)
                    {
                        falloffMapContain[x, y] = falloffMapEdge[x, y];
                    }

                    if (falloffAngle == 444)
                    {
                        falloffMapContain[x, y] = falloffMapExceptEdge[mapChunkSize + 1 - y, x];
                    }
                    if (falloffAngle == 333)
                    {
                        falloffMapContain[x, y] = falloffMapExceptEdge[mapChunkSize + 1 - y, mapChunkSize + 1 - x];
                    }
                    if (falloffAngle == 222)
                    {
                        falloffMapContain[x, y] = falloffMapExceptEdge[mapChunkSize + 1 - x, mapChunkSize + 1 - y];
                    }
                    if (falloffAngle == 111)
                    {
                        falloffMapContain[x, y] = falloffMapExceptEdge[x, y];
                    }

                    if (falloffAngle == 555)
                    {
                        falloffMapContain[x, y] = falloffMapDoubleEdge[y, x];
                    }
                    if (falloffAngle == 666)
                    {
                        falloffMapContain[x, y] = falloffMapDoubleEdge[x, y];
                    }
                    if (falloffAngle == 5)
                    {
                        falloffMapContain[x, y] = falloffMap[x, y];
                    }
                    if (falloffAngle == 0)
                    {

                    }
                    else
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - Mathf.Clamp01(falloffMapContain[x, y]));
                    }
                }
            }
        }

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 1; y < (mapChunkSize + 1); y++)
        {
            for (int x = 1; x < (mapChunkSize + 1); x++)
            {

                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight >= regions[i].height)
                    {
                        colourMap[(y - 1) * mapChunkSize + (x - 1)] = regions[i].colour;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return new MapData(noiseMap, colourMap);
    }

    public void SetIslandsMapSize(int widthLocation, int heightLocation) {
        this.widthLocation = widthLocation;
        this.heightLocation = heightLocation;
        System.Random prng = new System.Random(seed);
        islandsMap = Noise.GenerateNoiseMap(seed, new Vector2(prng.Next(-100000, 100000), prng.Next(-100000, 100000)), mapChunkSize * widthLocation + 2, mapChunkSize * heightLocation + 2, islandsNoiseScale, 2, persistance, lacunarity, normalizeMode);

        for (int y = 0; y < mapChunkSize * heightLocation + 2; y++)
        {
            for (int x = 0; x < mapChunkSize * widthLocation + 2; x++)
            {
                islandsMap[x, y] = Mathf.Clamp01((islandsMap[x, y] - immersionDepth) * (1 / coastalSlope) + immersionDepth);
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawTexture(TextureGenerator.TextureFromHeightMap(islandsMap));
    }

    void OnValidate()
    {
        if (octaves < 1)
        {
            octaves = 1;
        }
        if (octaves > 10)
        {
            octaves = 10;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (noiseScale < 1)
        {
            noiseScale = 1;
        }

        // 0 Вокруг, 1 с одной стороны, 2 с двух сторон(внешний угол), 3 кроме стороны, 4 с двух сторон
        falloffMapContain = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 0);
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 0);
        falloffMapEdge = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 1);
        falloffMapOuterCorner = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 2);
        falloffMapExceptEdge = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 3);
        falloffMapDoubleEdge = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2, 4);

    }

    struct MapThreadInfo<T> {
        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}

public struct MapData {
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    public MapData(float[,] heightMap, Color[] colourMap) {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}
