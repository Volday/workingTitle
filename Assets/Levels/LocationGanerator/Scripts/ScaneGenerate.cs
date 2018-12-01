using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaneGenerate : MonoBehaviour
{
    public GameObject plain;
    public GameObject plainCrissRoad;
    public GameObject plainRoad;
    public GameObject plainRoadFork;
    public GameObject plainRoadTurn;
    public int seed;
    public int mapLvl;
    public int mobLvl;
    public int[,] objectMap;
    public int[,] roadMap;
    List<Field> roadRoute = new List<Field>();

    // Use this for initialization
    void Start()
    {
        //Выбор сида
        Random.seed = Random.Range(0,9999999);
        seed = Random.seed;
        Debug.Log(seed);

        //Уровень сложности
        GenerateWorld();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Генерация мира
    private void GenerateWorld()
    {
        GenerateRoad();
        SpawnRoad();
    }

    // Генерация дорог
    private void GenerateRoad()
    {
        int lenghtOfRoad = (mapLvl) * (mapLvl) / 4;
        Field start = new Field(0, 0);
        roadRoute.Add(start);
        Queue<Field> nextRoad = new Queue<Field>();
        nextRoad.Enqueue(start);
        bool ready = true;
        bool thisReady = false;
        while (lenghtOfRoad > roadRoute.Count)
        {
            //Проверка сверху
            for (int t = -2; t < 3; t++)
            {
                if (!IsFree(nextRoad.Peek().x + t, nextRoad.Peek().y - 1))
                {
                    ready = false;
                    break;
                }
            }
            if (ready)
            {
                if (Random.Range(0, 2) == 1)
                {
                    roadRoute.Add(new Field(nextRoad.Peek().x, nextRoad.Peek().y - 1));
                    nextRoad.Enqueue(new Field(nextRoad.Peek().x, nextRoad.Peek().y - 1));
                }
                else
                {
                    thisReady = true;
                }
            }
            else
            {
                ready = true;
            }

            //Проверка снизу
            for (int t = -2; t < 3; t++)
            {
                if (!IsFree(nextRoad.Peek().x + t, nextRoad.Peek().y + 1))
                {
                    ready = false;
                    break;
                }
            }
            if (ready)
            {
                if (Random.Range(0, 2) == 1)
                {
                    roadRoute.Add(new Field(nextRoad.Peek().x, nextRoad.Peek().y + 1));
                    nextRoad.Enqueue(new Field(nextRoad.Peek().x, nextRoad.Peek().y + 1));
                }
                else
                {
                    thisReady = true;
                }
            }
            else
            {
                ready = true;
            }

            //Проверка слева
            for (int t = -2; t < 3; t++)
            {
                if (!IsFree(nextRoad.Peek().x - 1, nextRoad.Peek().y + t))
                {
                    ready = false;
                    break;
                }
            }
            if (ready)
            {
                if (Random.Range(0, 2) == 1)
                {
                    roadRoute.Add(new Field(nextRoad.Peek().x - 1, nextRoad.Peek().y));
                    nextRoad.Enqueue(new Field(nextRoad.Peek().x - 1, nextRoad.Peek().y));
                }
                else
                {
                    thisReady = true;
                }
            }
            else
            {
                ready = true;
            }

            //Проверка справа
            for (int t = -2; t < 3; t++)
            {
                if (!IsFree(nextRoad.Peek().x + 1, nextRoad.Peek().y + t))
                {
                    ready = false;
                    break;
                }
            }
            if (ready)
            {
                if (Random.Range(0, 2) == 1)
                {
                    roadRoute.Add(new Field(nextRoad.Peek().x + 1, nextRoad.Peek().y));
                    nextRoad.Enqueue(new Field(nextRoad.Peek().x + 1, nextRoad.Peek().y));
                }
                else
                {
                    thisReady = true;
                }
            }
            else
            {
                ready = true;
            }

            if (thisReady)
            {
                nextRoad.Enqueue(new Field(nextRoad.Peek().x, nextRoad.Peek().y));
            }
            thisReady = false;

            nextRoad.Dequeue();

        }

        int minHight = 0;
        int maxHight = 0;
        int minLong = 0;
        int maxLong = 0;
        for (int t = 0;t<roadRoute.Count ;t++) {
            if (roadRoute[t].x < minLong) {
                minLong = roadRoute[t].x;
            }
            if (roadRoute[t].x > maxLong)
            {
                maxLong = roadRoute[t].x;
            }
            if (roadRoute[t].y < minHight)
            {
                minHight = roadRoute[t].y;
            }
            if (roadRoute[t].y > maxHight)
            {
                maxHight = roadRoute[t].y;
            }
        }
        roadMap = new int[(maxLong - minLong) + 1, (maxHight - minHight) + 1];
        for (int t = 0;t<roadRoute.Count ;t++) {
            roadMap[roadRoute[t].x - minLong, roadRoute[t].y - minHight] = 1;
        }
    }

    //Создание дорог
    private void SpawnRoad() {
        int lengthRoad = roadMap.GetLength(0);
        int widthRoad = roadMap.GetLength(1);
        for (int t = 0;t < lengthRoad ;t++) {
            for (int i = 0; i < widthRoad; i++)
            {
                if (roadMap[t, i] == 0) {
                    Instantiate(plain, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                    continue;
                }
                //Обработка углов
                if (t == 0 && i == 0) {
                    if (roadMap[t, i + 1] == 1 && roadMap[t + 1, i] == 1) {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 1 && roadMap[t + 1, i] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 0 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                }
                if (t == 0 && i == widthRoad - 1)
                {
                    if (roadMap[t, i - 1] == 1 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 1 && roadMap[t + 1, i] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 0 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                }
                if (t == lengthRoad - 1 && i == 0)
                {
                    if (roadMap[t, i + 1] == 1 && roadMap[t - 1, i] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, -90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 1 && roadMap[t - 1, i] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 0 && roadMap[t - 1, i] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                }
                if (t == lengthRoad - 1 && i == widthRoad - 1)
                {
                    if (roadMap[t, i - 1] == 1 && roadMap[t - 1, i] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 180));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 1 && roadMap[t - 1, i] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 0 && roadMap[t - 1, i] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                }
                //Обработка краёв
                if (t == 0) {
                    if (roadMap[t + 1, i] == 1 && roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 1) {
                        Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, 0));
                        continue;
                    }
                    if (roadMap[t + 1, i] == 1 && roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 0)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t + 1, i] == 1 && roadMap[t, i - 1] == 0 && roadMap[t, i + 1] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                    if (roadMap[t + 1, i] == 0 && ((roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 0) || (roadMap[t, i - 1] == 0 && roadMap[t, i + 1] == 1)))
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, 0));
                        continue;
                    }
                    if (roadMap[t + 1, i] == 1 && roadMap[t, i - 1] == 0 && roadMap[t, i + 1] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                    if (roadMap[t + 1, i] == 0 && roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, 0));
                        continue;
                    }
                }
                if (t == lengthRoad - 1)
                {
                    if (roadMap[t - 1, i] == 1 && roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 1)
                    {
                        Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 90, 0));
                        continue;
                    }
                    if (roadMap[t - 1, i] == 1 && roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 0)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 180));
                        continue;
                    }
                    if (roadMap[t - 1, i] == 1 && roadMap[t, i - 1] == 0 && roadMap[t, i + 1] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t - 1, i] == 0 && ((roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 0) || (roadMap[t, i - 1] == 0 && roadMap[t, i + 1] == 1)))
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, 0));
                        continue;
                    }
                    if (roadMap[t - 1, i] == 1 && roadMap[t, i - 1] == 0 && roadMap[t, i + 1] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                    if (roadMap[t - 1, i] == 0 && roadMap[t, i - 1] == 1 && roadMap[t, i + 1] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, 0));
                        continue;
                    }
                }
                if (i == 0)
                {
                    if (roadMap[t, i + 1] == 1 && roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, -90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 1 && roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 0)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, -90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 1 && roadMap[t - 1, i] == 0 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 0 && ((roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 0) || (roadMap[t - 1, i] == 0 && roadMap[t + 1, i] == 1)))
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, -90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 1 && roadMap[t - 1, i] == 0 && roadMap[t + 1, i] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, -90));
                        continue;
                    }
                    if (roadMap[t, i + 1] == 0 && roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, -90));
                        continue;
                    }
                }
                if (i == widthRoad - 1)
                {
                    if (roadMap[t, i - 1] == 1 && roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 90, -90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 1 && roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 0)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 180));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 1 && roadMap[t - 1, i] == 0 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 0 && ((roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 0) || (roadMap[t - 1, i] == 0 && roadMap[t + 1, i] == 1)))
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, -90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 1 && roadMap[t - 1, i] == 0 && roadMap[t + 1, i] == 0)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, -90));
                        continue;
                    }
                    if (roadMap[t, i - 1] == 0 && roadMap[t - 1, i] == 1 && roadMap[t + 1, i] == 1)
                    {
                        Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, -90, -90));
                        continue;
                    }
                }
                //Обработка центральных полей
                if (roadMap[t + 1, i] == 1 && roadMap[t - 1, i] == 1 && roadMap[t, i + 1] == 1 && roadMap[t, i - 1] == 1) {
                    Instantiate(plainCrissRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                    continue;
                }
                if ((roadMap[t + 1, i] == 1 || roadMap[t - 1, i] == 1) && (roadMap[t, i + 1] == 0 && roadMap[t, i - 1] == 0))
                {
                    Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                    continue;
                }
                if ((roadMap[t + 1, i] == 0 && roadMap[t - 1, i] == 0) && (roadMap[t, i + 1] == 1 || roadMap[t, i - 1] == 1))
                {
                    Instantiate(plainRoad, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                    continue;
                }
                if (roadMap[t + 1, i] == 0 && roadMap[t - 1, i] == 1 && roadMap[t, i + 1] == 1 && roadMap[t, i - 1] == 1)
                {
                    Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                    continue;
                }
                if (roadMap[t + 1, i] == 1 && roadMap[t - 1, i] == 0 && roadMap[t, i + 1] == 1 && roadMap[t, i - 1] == 1)
                {
                    Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, -90));
                    continue;
                }
                if (roadMap[t + 1, i] == 1 && roadMap[t - 1, i] == 1 && roadMap[t, i + 1] == 0 && roadMap[t, i - 1] == 1)
                {
                    Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                    continue;
                }
                if (roadMap[t + 1, i] == 1 && roadMap[t - 1, i] == 1 && roadMap[t, i + 1] == 1 && roadMap[t, i - 1] == 0)
                {
                    Instantiate(plainRoadFork, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 180));
                    continue;
                }
                if (roadMap[t + 1, i] == 0 && roadMap[t - 1, i] == 1 && roadMap[t, i + 1] == 0 && roadMap[t, i - 1] == 1)
                {
                    Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 180));
                    continue;
                }
                if (roadMap[t + 1, i] == 0 && roadMap[t - 1, i] == 1 && roadMap[t, i + 1] == 1 && roadMap[t, i - 1] == 0)
                {
                    Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 270));
                    continue;
                }
                if (roadMap[t + 1, i] == 1 && roadMap[t - 1, i] == 0 && roadMap[t, i + 1] == 0 && roadMap[t, i - 1] == 1)
                {
                    Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 90));
                    continue;
                }
                if (roadMap[t + 1, i] == 1 && roadMap[t - 1, i] == 0 && roadMap[t, i + 1] == 1 && roadMap[t, i - 1] == 0)
                {
                    Instantiate(plainRoadTurn, new Vector3(0 + t * 4, 0, 0 + i * 4), Quaternion.Euler(-90, 0, 0));
                    continue;
                }
            }
        }
    }

    bool IsFree(int x, int y)
    {
        for (int t = 0; t < roadRoute.Count; t++)
        {
            if (roadRoute[t].x == x && roadRoute[t].y == y)
            {
                return false;
            }
        }
        return true;
    }

    public class Field
    {
        public int x;
        public int y;
        public Field(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
