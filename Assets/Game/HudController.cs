using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class HudController : MonoBehaviour {

  public TextMeshProUGUI massText;

  private PlayerController playerController;

  private void Start() {
    // Locate the player
    playerController = FindObjectOfType<PlayerController>();
    Assert.IsNotNull(playerController, "Failed to located a PlayerController");

    // Set the proper text formatting for Mass
    massText.formatText = "Mass: {0}";

    // Initialize the mass
    OnMassChanged(0, 0);

    // Register for events
    playerController.OnMassChanged += OnMassChanged;
  }

  private void OnDestroy() {
    // Unregister for events
    playerController.OnMassChanged -= OnMassChanged;
  }
    
  public void OnMassChanged(float initialMass, float mass) {
    massText.LerpToTargetValue((int)initialMass, (int)mass);
  }
}
