using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A player in idle cannot respond to touches. Used at the end of the game

public class PlayerIdleState : PlayerState {

  public PlayerIdleState(PlayerController controller) : base(controller) {
    stateName = "Idle";
  }
}
