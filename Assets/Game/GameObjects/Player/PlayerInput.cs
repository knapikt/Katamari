using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerInput : MonoBehaviour {

  private PlayerController playerController;
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
