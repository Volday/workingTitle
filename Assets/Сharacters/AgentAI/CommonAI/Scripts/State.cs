using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{

    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        int maxDecision = int.MinValue;
        int bestTransition = -1;
        for (int i = 0; i < transitions.Length; i++)
        {
            int newDecision = transitions[i].decision.Decide(controller);
            if (maxDecision < newDecision) {
                maxDecision = newDecision;
                bestTransition = i;
            }
        }

        if (bestTransition != -1)
        {
            controller.TransitionToState(transitions[bestTransition].newState);
        }
    }
}
