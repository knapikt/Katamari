using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A state for the GameController

public class GameState : ComponentState<GameController> {

  public GameState(GameController controller) : base(controller) {
    stateName = "GameState";
  }

}
