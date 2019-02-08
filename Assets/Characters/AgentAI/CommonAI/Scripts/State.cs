using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    [Range(0.001f, 3)]
    public float transitionsUpdateFrequency = 0.001f;
    [Range(0.001f, 3)]
    public float actionsUpdateFrequency = 0.001f;
    float remainTimeForTransitions = 100;
    float remainTimeForActions = 100;

    public ActionInState[] actions;
    public Transition[] transitions;


    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        if (remainTimeForActions > actionsUpdateFrequency) {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
            remainTimeForActions = 0;
        }
        else
        {
            remainTimeForActions += Time.deltaTime;
        }
    }

    private void CheckTransitions(StateController controller)
    {
        if (remainTimeForTransitions > transitionsUpdateFrequency)
        {
            float maxDecision = float.MinValue;
            int bestTransition = -1;
            for (int i = 0; i < transitions.Length; i++)
            {
                float newDecision = transitions[i].decision.Decide(controller);
                if (maxDecision < newDecision)
                {
                    maxDecision = newDecision;
                    bestTransition = i;
                    if (newDecision == float.MaxValue) {
                        break;
                    }
                }
            }

            if (bestTransition != -1)
            {
                controller.TransitionToState(transitions[bestTransition].newState);
            }
            remainTimeForTransitions = 0;
        }
        else {
            remainTimeForTransitions += Time.deltaTime;
        }
    }
}
