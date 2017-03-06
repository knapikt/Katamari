using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class HudController : MonoBehaviour {

  public TextMeshProUGUI quotaText;
  public TextMeshProUGUI massText;
  public FillBarController healthFillBarController;

  private PlayerController playerController;
  private GameController   gameController;

  private void Start() {
    // Locate the player
    playerController = FindObjectOfType<PlayerController>();
    Assert.IsNotNull(playerController, "Failed to located a PlayerController");

    gameController = FindObjectOfType<GameController>();
    Assert.IsNotNull(gameController, "Failed to located a GameController");

    // Set the proper text formatting for Mass
    massText.formatText = "Mass: {0}";
   
    // Initialize the hud display
    OnMassChanged(0, 0);
    OnAttachCountChanged(playerController.AttachCount);

    // Animate initial health
    healthFillBarController.SetPercentFilled(0, false);
    healthFillBarController.SetPercentFilled(playerController.HealthPercent);

    // Register for events
    playerController.OnAttachedCountChanged += OnAttachCountChanged;
    playerController.OnMassChanged          += OnMassChanged;
    playerController.OnHealthPercentChanged += OnHealthPercentChanged;
  }

  private void OnDestroy() {
    // Unregister for events
    playerController.OnAttachedCountChanged -= OnAttachCountChanged;
    playerController.OnMassChanged          -= OnMassChanged;
    playerController.OnHealthPercentChanged -= OnHealthPercentChanged;
  }
    
  public void OnMassChanged(float initialMass, float mass) {
    massText.LerpToTargetValue((int)initialMass, (int)mass);
  }

  public void OnHealthPercentChanged(float initialHealth, float health) {
    healthFillBarController.SetPercentFilled(health);
  }

  public void OnAttachCountChanged(int count) {
    count = Mathf.Clamp(count, 0, gameController.AttachableQuota);
    quotaText.text = string.Format("Collect: {0} / {1}", count, gameController.AttachableQuota);
  }
}
