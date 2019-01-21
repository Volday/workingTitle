﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "AgentAI/CommonAI/Decisions")]
public abstract class Decision : ScriptableObject
{
    public abstract int Decide(StateController controller);
}
