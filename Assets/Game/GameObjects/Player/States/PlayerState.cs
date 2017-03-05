﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ComponentState<PlayerController> {


  public PlayerState(PlayerController controller) : base(controller) {
    stateName = "PlayerState";
  }

  public virtual void Move(Vector3 direction) { }
  public virtual void Jump()                  { }

  public virtual void OnGroundCollision(Collision collision, GroundController groundController) { }

  public virtual void OnAttachableCollision(Collision collision, AttachableController attachableController) { 
    controller.Attach(attachableController);
    attachableController.Attach(controller);
  }
}
