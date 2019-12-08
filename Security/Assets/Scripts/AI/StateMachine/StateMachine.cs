using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateType ActiveStateType { get { return activeState.StateType; } }

    private Dictionary<StateType, IState> iStateByStateTypes = new Dictionary<StateType, IState>();
    private IState activeState;
    private Coroutine updateStateCoroutine;
    private bool initiated;

    public void SwitchState(StateType stateType)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }

        if (!initiated)
        {
            Initiate();
        }

        activeState = iStateByStateTypes[stateType];
        activeState.Enter();

        if (updateStateCoroutine == null)
        {
            updateStateCoroutine = StartCoroutine(UpdateState());
        }
    }

    public void Stop()
    {
        if (updateStateCoroutine != null)
        {
            StopCoroutine(updateStateCoroutine);
            updateStateCoroutine = null;
        }

        if (activeState != null)
        {
            activeState.Exit();
            activeState = null;
        }
    }

    private IEnumerator UpdateState()
    {
        while (true)
        {
            activeState.Act();
            yield return null;
        }
    }

    private void Initiate()
    {
        List<IState> states = GetComponents<IState>().ToList();

        foreach (IState state in states)
        {
            iStateByStateTypes.Add(state.StateType, state);
        }

        initiated = true;
    }
}