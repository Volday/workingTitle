using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeGeneratorType1 : MonoBehaviour {

    const float scale = 1f;

    public int widthLocation;
    public int heightLocation;

    [Range(0, 1)]
    public float maxFreeHeight;
    [Range(0, 1)]
    public float minFreeHeight;

    public bool hideRemoteChunks;

    const float viewerMoveThresholdForChankUpdate = 25f;
    const float sqrViewerMoveThresholdForChankUpdate = viewerMoveThresholdForChankUpdate * viewerMoveThresholdForChankUpdate;

    public float maxVievDst;

    public Color colorRoad;

    [Range(10, 90)]
    public float angle;
    public float stapLenght;
    public float widthRoad;
    public float minDistanceBetweenRoads;
    public int minRoadLenght;

    public Transform viewer;
    public Material mapMaterial;

    public static Vector2 viewerPosition;
    Vector2 viewerPositionOld;
    static MapGenerator mapGenerator;
    static LandscapeGeneratorType1 locationGeneratorType1;
    int chunkSize;
    int ChunkVisibleInViewDst;

    int[,] falloffAngleArray;

    static int mapDataLoaded = 0;
    static float[,] wholeMapData;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    

    void Update()
    {
        if (hideRemoteChunks) {
            viewerPosition = new Vector2(viewer.position.x, viewer.position.z) / scale;

            if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChankUpdate)
            {
                viewerPositionOld = viewerPosition;
                UpdateVisibleChunks();
            }
        }
    }

    public void LandscapeGenerate()
    {
        locationGeneratorType1 = this;

        mapGenerator = FindObjectOfType<MapGenerator>();

        CountFalloffAngleArray();

        chunkSize = MapGenerator.mapChunkSize - 1;

        wholeMapData = new float[chunkSize * widthLocation, chunkSize * heightLocation];

        LoadNextChunks();

        if (hideRemoteChunks)
        {
            ChunkVisibleInViewDst = Mathf.RoundToInt(maxVievDst / chunkSize);
            UpdateVisibleChunks();
        }
    }

    void CountFalloffAngleArray() {
        falloffAngleArray = new int[widthLocation, heightLocation];

        for (int y = 0; y < heightLocation; y++) {
            for (int x = 0; x < widthLocation; x++)
            {
                if (heightLocation == 1 && widthLocation == 1)
                {
                    falloffAngleArray[x, y] = 5;
                    continue;
                }
                if (heightLocation == 1 && widthLocation != 1 && x == 0)
                {
                    falloffAngleArray[x, y] = 333;
                    continue;
                }
                if (heightLocation == 1 && widthLocation != 1 && x == widthLocation - 1)
                {
                    falloffAngleArray[x, y] = 444;
                    continue;
                }
                if (heightLocation != 1 && widthLocation == 1 && y == 0)
                {
                    falloffAngleArray[x, y] = 111;
                    continue;
                }
                if (heightLocation != 1 && widthLocation == 1 && y == heightLocation - 1)
                {
                    falloffAngleArray[x, y] = 222;
                    continue;
                }
                if (heightLocation != 1 && widthLocation == 1 && y != widthLocation - 1 && y != 0)
                {
                    falloffAngleArray[x, y] = 666;
                    continue;
                }
                if (heightLocation == 1 && widthLocation != 1 && x != widthLocation - 1 && x != 0)
                {
                    falloffAngleArray[x, y] = 555;
                    continue;
                }
                if (x != 0 && y != 0 && x != widthLocation - 1 && y != heightLocation - 1)
                {
                    falloffAngleArray[x, y] = 0;
                    continue;
                }
                if (x == 0 && y == 0)
                {
                    falloffAngleArray[x, y] = 33;
                    continue;
                }
                if (x == widthLocation - 1 && y == 0)
                {
                    falloffAngleArray[x, y] = 11;
                    continue;
                }
                if (x == 0 && y == heightLocation - 1)
                {
                    falloffAngleArray[x, y] = 22;
                    continue;
                }
                if (x == widthLocation - 1 && y == heightLocation - 1)
                {
                    falloffAngleArray[x, y] = 44;
                    continue;
                }
                if (x == 0)
                {
                    falloffAngleArray[x, y] = 3;
                    continue;
                }
                if (x == widthLocation - 1)
                {
                    falloffAngleArray[x, y] = 1;
                    continue;
                }
                if (y == 0)
                {
                    falloffAngleArray[x, y] = 2;
                    continue;
                }
                if (y == heightLocation - 1)
                {
                    falloffAngleArray[x, y] = 4;
                    continue;
                }
            }
        }
    }

    void LoadNextChunks() {
        Vector2 chunkCoord = new Vector2(mapDataLoaded % widthLocation, mapDataLoaded / widthLocation);
        terrainChunkDictionary.Add(chunkCoord, new TerrainChunk(hideRemoteChunks, maxVievDst, chunkCoord, chunkSize, transform, mapMaterial, falloffAngleArray[(int)chunkCoord.x, (int)chunkCoord.y]));
    }

    void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }
        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -ChunkVisibleInViewDst; yOffset <= ChunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -ChunkVisibleInViewDst; xOffset <= ChunkVisibleInViewDst; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (currentChunkCoordY + yOffset >= 0 && currentChunkCoordX + xOffset >= 0 && currentChunkCoordX + xOffset < widthLocation && currentChunkCoordY + yOffset < heightLocation) {
                    if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    }
                    else
                    {
                        terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(hideRemoteChunks, maxVievDst, viewedChunkCoord, chunkSize, transform, mapMaterial, falloffAngleArray[0, 0]));
                    }
                }
            }
        }
    }

    public void CheckLoadedWholeMapData() {
        if (mapDataLoaded == widthLocation * heightLocation)
        {
            int[,] coverageMap = new int[chunkSize * widthLocation, chunkSize * heightLocation];
            for (int y = 0; y < heightLocation * chunkSize; y++) {
                for (int x = 0; x < widthLocation * chunkSize; x++)
                {
                    if (wholeMapData[x, y] > minFreeHeight && wholeMapData[x, y] < maxFreeHeight)
                    {
                        coverageMap[x, y] = 0; //свободно
                    }
                    else {
                        coverageMap[x, y] = 1; //занято ландшафтом
                    }
                }
            }

            RoadGenerator roadGenerator = new RoadGenerator(wholeMapData, coverageMap, minFreeHeight, maxFreeHeight, angle, stapLenght, widthRoad, mapGenerator.seed, minDistanceBetweenRoads, minRoadLenght);
            coverageMap = roadGenerator.RoadGenerateFromNoise();

            for (int y = 0; y < heightLocation; y++)
            {
                for (int x = 0; x < widthLocation; x++)
                {
                    terrainChunkDictionary[new Vector2(x, y)].RoadTextureUpdate(colorRoad, coverageMap);
                }
            }

            MapDisplay display = FindObjectOfType<MapDisplay>();
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(coverageMap, 2));
        }
        else {
            LoadNextChunks();
        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        MeshCollider meshCollider;

        MapData mapData;
        bool mapDataReceived;
        float maxVievDst;
        bool hideRemoteChunks;

        Mesh mesh;
        bool hasRequestedMesh;
        bool hasMesh;

        int chunkSize;
        Vector2 coord;

        public TerrainChunk(bool hideRemoteChunks, float maxVievDst, Vector2 coord, int size, Transform parent, Material material, int falloffAngle)
        {
            this.chunkSize = size;
            this.coord = coord;

            this.maxVievDst = maxVievDst;
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshCollider = meshObject.AddComponent<MeshCollider>();
            meshRenderer.material = material;

            meshObject.transform.position = positionV3 * scale;
            meshObject.transform.parent = parent;
            meshObject.transform.localScale = Vector3.one * scale;
            SetVisible(false);

            mapGenerator.RequestMapData(position, OnMapDataReceived, falloffAngle);
        }

        void OnMapDataReceived(MapData mapData)
        {
            this.mapData = mapData;
            mapDataReceived = true;

            Texture2D texture = TextureGenerator.TextureFromColourMap(mapData.colourMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize);
            meshRenderer.material.mainTexture = texture;
            
            for (int i = 0; i < chunkSize; i++) {
                for (int t = 0; t < chunkSize; t++)
                {
                    wholeMapData[wholeMapData.GetLength(0) - 1 - (t + chunkSize * (int)coord.x), wholeMapData.GetLength(1) - 1 - ((chunkSize - 1 - i) + chunkSize * (int)coord.y )] = mapData.heightMap[(t + 1), (i + 1)];
                }
            }

            mapDataLoaded++;

            UpdateTerrainChunk();
        }

        public void RoadTextureUpdate(Color colorRoad, int[,] coverageMap) {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int x = 0; x < chunkSize; x++)
                {
                    if (coverageMap[coverageMap.GetLength(0) - (x + chunkSize * (int)coord.x) - 1, coverageMap.GetLength(0) - ((chunkSize - 1 - y) + chunkSize * (int)coord.y) - 1] == 2)
                    {
                        mapData.colourMap[y * MapGenerator.mapChunkSize + x] = colorRoad;
                    }
                }
            }
            Texture2D texture = TextureGenerator.TextureFromColourMap(mapData.colourMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize);
            meshRenderer.material.mainTexture = texture;
        }

        public void UpdateTerrainChunk()
        {
            if (mapDataReceived)
            {
                bool visible = true;
                if (hideRemoteChunks) {
                    float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
                    visible = viewerDstFromNearestEdge <= maxVievDst;
                }

                if (visible)
                {
                    if (hasMesh)
                    {
                        meshFilter.mesh = mesh;
                        meshCollider.sharedMesh = mesh;

                        locationGeneratorType1.CheckLoadedWholeMapData();
                    }
                    else if (!hasRequestedMesh)
                    {
                        RequestMesh(mapData);
                    }

                    terrainChunksVisibleLastUpdate.Add(this);
                }
                SetVisible(visible);
            }
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            hasMesh = true;
            mesh = meshData.CreateMesh();
            UpdateTerrainChunk();
        }

        public void RequestMesh(MapData mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, 0, OnMeshDataReceived);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }

    void OnValidate() {
        if (widthLocation < 1)
        {
            widthLocation = 1;
        }
        if (heightLocation < 1)
        {
            heightLocation = 1;
        }
        if (maxFreeHeight < minFreeHeight) {
            minFreeHeight = maxFreeHeight;
        }
        if (minDistanceBetweenRoads > minRoadLenght * stapLenght - stapLenght) {
            minDistanceBetweenRoads = minRoadLenght * stapLenght - stapLenght;
        }
    }
}
