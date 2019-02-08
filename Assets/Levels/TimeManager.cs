using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public float gameTime;
    List<ActionToDo> actionsToDo = new List<ActionToDo>();

    void Start()
    {
        gameTime = 0;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        for (int t = 0; t < actionsToDo.Count; t++) {
            if (actionsToDo[t].timeToStart < gameTime)
            {
                if (actionsToDo[t].gameObject != null && actionsToDo[t].action != null) {
                    actionsToDo[t].action();
                }
                actionsToDo.Remove(actionsToDo[t]);
            }
            else {
                break;
            }
        }
    }

    public void AddAction(Action action, float waitingTime, Component _gameObject) {
        float timeToStart = waitingTime + gameTime;
        int indexForInsert = 0;
        ActionToDo actionToDo = new ActionToDo(action, timeToStart, _gameObject);
        for (int t = 0; t < actionsToDo.Count; t++)
        {
            if (actionsToDo[t].timeToStart > timeToStart)
            {
                indexForInsert = t;
                break;
            }
        }
        if (indexForInsert == 0)
        {
            actionsToDo.Add(actionToDo);
        }
        else {
            actionsToDo.Insert(indexForInsert, actionToDo);
        }
    }

    class ActionToDo {
        public float timeToStart;
        public Action action;
        public Component gameObject;
        public ActionToDo(Action _action, float _timeToStart, Component _gameObject) {
            timeToStart = _timeToStart;
            action = _action;
            gameObject = _gameObject;
        }
    }
}
