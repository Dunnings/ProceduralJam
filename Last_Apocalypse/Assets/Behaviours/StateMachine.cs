using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public enum State
    {
        stateMenu,
        statePlay,
        statePause,
        stateGameOver
    }

    public static StateMachine Instance;
    public static State s_currentState;

    // Use this for initialization
    void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeState(State _newState)
    {
        switch (s_currentState)
        {
            case State.stateMenu:
                switch (_newState)
                {
                    case State.stateMenu:
                        break;
                    case State.statePlay:
                        break;
                    case State.statePause:
                        break;
                    case State.stateGameOver:
                        break;
                    default:
                        break;
                }
                break;
            case State.statePlay:
                switch (_newState)
                {
                    case State.stateMenu:
                        break;
                    case State.statePlay:
                        break;
                    case State.statePause:
                        break;
                    case State.stateGameOver:
                        break;
                    default:
                        break;
                }
                break;
            case State.statePause:
                switch (_newState)
                {
                    case State.stateMenu:
                        break;
                    case State.statePlay:
                        break;
                    case State.statePause:
                        break;
                    case State.stateGameOver:
                        break;
                    default:
                        break;
                }
                break;
            case State.stateGameOver:
                switch (_newState)
                {
                    case State.stateMenu:
                        break;
                    case State.statePlay:
                        break;
                    case State.statePause:
                        break;
                    case State.stateGameOver:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        s_currentState = _newState;
    }
}
