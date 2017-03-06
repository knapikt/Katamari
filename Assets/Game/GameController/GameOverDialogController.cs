using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// A dialog showing that the game is over

public class GameOverDialogController : DialogController {

  public TextMeshProUGUI outcomeText;
  public Button          restartButton;

  private void Start() {
    restartButton.onClick.AddListener(OnRestartClick);
  }

  public void Show(bool won) {
    if (won) {
      outcomeText.text = "You win!";
    } else {
      outcomeText.text = "You lost!";
    }

    Show();
  }

  private void OnRestartClick() {
    SceneController.Instance.LoadLevel(Scenes.Game);
    Dismiss();
  }
}
