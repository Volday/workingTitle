using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//от этого класса наследуются вспомогательные компоненты от способностей, их можно будет просто убрать при отмене способности.
public abstract class AbilityComponent : MonoBehaviour
{
    public abstract void CastEnd();
}
