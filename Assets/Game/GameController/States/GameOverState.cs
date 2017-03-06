using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Runs when the player has died or won the game

public class GameOverState : GameState {

  public GameOverState(GameController controller) : base(controller) {
    stateName = "GameOverState";
  }

  public override void Enter() {
    controller.PlayerController.State = controller.PlayerController.IdleState;

    if (controller.PlayerController.Alive) {
      HandleWin();
    } else {
      HandleLoss();
    }
  }

  private void HandleWin() {
    SoundController.Instance.PlayMusicAudioClip(controller.GameSounds.musicGameOverWin, false);
    var dialogController = DialogController.Retrieve<GameOverDialogController>();
    dialogController.Show(true);
  }

  private void HandleLoss() {
    controller.PlayerController.ExplodeAttached(1.0f);
    SoundController.Instance.PlayMusicAudioClip(controller.GameSounds.musicGameOverLoss, false);
    var dialogController = DialogController.Retrieve<GameOverDialogController>();
    dialogController.Show(false);
  }
}
