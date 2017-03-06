using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : ComponentState<GameController> {

  public GameState(GameController controller) : base(controller) {
    stateName = "GameState";
  }

}
