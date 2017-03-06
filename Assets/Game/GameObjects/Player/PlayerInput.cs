using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A script that is attached to the PlayerController that forwards input information to the PlayerController

public class PlayerInput : MonoBehaviour {

  // Reference to the player
  private PlayerController playerController;

  // A resused vector for determining which way to rotate the player
  private Vector3 direction = Vector3.zero;

  public void Awake() {
    playerController = gameObject.GetComponent<PlayerController>();
    Assert.IsNotNull(playerController, "Failed to locate a controller");
  }

	public void Update() {
    // Attempt to jump first
    if (Input.GetKeyDown(KeyCode.Space)) {
      playerController.Jump();
      return;
    }

    // Reset direction
    direction = Vector3.zero;

    if (Input.GetKey(KeyCode.A)) { direction += Vector3.forward;    }
    if (Input.GetKey(KeyCode.W)) { direction += Vector3.right; }
    if (Input.GetKey(KeyCode.D)) { direction += Vector3.back;   }
    if (Input.GetKey(KeyCode.S)) { direction += Vector3.left;    }

    playerController.Move(direction);
	}
}
