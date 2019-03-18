using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMove : MonoBehaviour
{
    public GameObject trackedObject;
    public Camera camera;
    public Vector3 cameraOffset;
    public float updateTime;
    public float searchRadius;
    TimeManager timeManager;
    public float smoothSpeed = 0.125f;
    EnemiesAround enemiesAround;
    List<GameObject> enemies;
    LastStaps lastStaps;
    int stapsCount = 3;

    void Start()
    {
        trackedObject = GameObject.FindGameObjectWithTag("Player");
        enemiesAround = trackedObject.GetComponent<EnemiesAround>();
        CameraMove cameraMoveComponent = camera.GetComponent<CameraMove>();
        cameraMoveComponent.cameraOffset = cameraOffset;
        cameraMoveComponent.smoothSpeed = smoothSpeed;
        cameraMoveComponent.target = gameObject.transform;
        Instantiate(camera, transform.position, Quaternion.identity);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        lastStaps = trackedObject.GetComponent<LastStaps>();
        stapsCount = (int)(0.8f / lastStaps.timeBetweenSteps);

        UpdateCameraTargetPosition();
    }

    void Update()
    {
        Vector3 positionBetween = new Vector3(0, 0, 0);
        Vector3 frontPosition = Vector3.zero;
        if (enemies.Count > 0)
        {
            for (int t = 0; t < enemies.Count; t++)
            {
                positionBetween.x += enemies[t].transform.position.x;
                positionBetween.y += enemies[t].transform.position.y;
                positionBetween.z += enemies[t].transform.position.z;
            }
            positionBetween = positionBetween / enemies.Count;
            positionBetween = Vector3.Lerp(positionBetween, trackedObject.transform.position, 0.6f);
        }
        else {
            positionBetween = trackedObject.transform.position;
            frontPosition = Vector3.Lerp(lastStaps.steps[lastStaps.steps.Count - 1] - lastStaps.steps[lastStaps.steps.Count - stapsCount * 2 - 1],
            Vector3.zero, 1.0f - 1.0f / 2.0f);
        }

        transform.position = positionBetween + frontPosition;
    }

    void UpdateCameraTargetPosition() {
        List<GameObject> newEnemies = new List<GameObject>();
        enemies = enemiesAround.FindEnemiesAround(searchRadius);
        for (int t = 0; t < enemies.Count; t++) {
            newEnemies.Add(enemies[t]);
        }
        enemies = newEnemies;
        timeManager.AddAction(UpdateCameraTargetPosition, updateTime, this);
    }
}
