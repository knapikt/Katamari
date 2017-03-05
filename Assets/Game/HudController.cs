using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class HudController : MonoBehaviour {

  public TextMeshProUGUI massText;
  public FillBarController healthFillBarController;

  private PlayerController playerController;

  private void Start() {
    // Locate the player
    playerController = FindObjectOfType<PlayerController>();
    Assert.IsNotNull(playerController, "Failed to located a PlayerController");

    // Set the proper text formatting for Mass
    massText.formatText = "Mass: {0}";

    // Initialize the mass
    OnMassChanged(0, 0);

    // Animate initial health
    healthFillBarController.SetPercentFilled(0, false);
    healthFillBarController.SetPercentFilled(playerController.HealthPercent);

    // Register for events
    playerController.OnMassChanged          += OnMassChanged;
    playerController.OnHealthPercentChanged += OnHealthPercentChanged;
  }

  private void OnDestroy() {
    // Unregister for events
    playerController.OnMassChanged -= OnMassChanged;
    playerController.OnHealthPercentChanged -= OnHealthPercentChanged;
  }
    
  public void OnMassChanged(float initialMass, float mass) {
    massText.LerpToTargetValue((int)initialMass, (int)mass);
  }

  public void OnHealthPercentChanged(float initialHealth, float health) {
    Debug.Log(string.Format("Changed: {0}", health));
    healthFillBarController.SetPercentFilled(health);
  }
}
