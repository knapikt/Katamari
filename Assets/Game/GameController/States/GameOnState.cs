using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOnState : GameState {

  // Runs while the game is not over

  public GameOnState(GameController controller) : base(controller) {
    stateName = "GameOnState";
  }

  public override void Enter() {
    SoundController.Instance.PlayMusicAudioClip(controller.GameSounds.musicMain);
  }

  public override void Update() {
    if (controller.PlayerController.AttachCount >= controller.AttachableQuota ||
       !controller.PlayerController.Alive) {
      controller.State = controller.GameOver;
    }
  }

}
