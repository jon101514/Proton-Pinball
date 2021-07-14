using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	// SINGLETON
    public static StateManager instance;

    // PUBLIC
    public enum GameState {
    	PLACEMENT,
    	GAME, 
    	INDEX_TWO,
    	OVER
    }

    // PRIVATE
    private GameState state; 	// 0 - Placement
    					// 1 - Game

    private void Awake() {
    	if (instance == null) { instance = this; }
    	state = GameState.PLACEMENT;
    }

    public void ChangeState() {
		state++;
		if (state == GameState.OVER) {
			state = GameState.PLACEMENT;
		}
    	// DebugText.instance.ChangeText("CURRENT STATE: " + state.ToString());
    	EvaluateState();
    }

    private void EvaluateState() {
    	// #TODO
    }

    public GameState GetState() { return state; }
}
