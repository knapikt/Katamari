using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A base player state

public class PlayerState : ComponentState<PlayerController> {
 
  public PlayerState(PlayerController controller) : base(controller) {
    stateName = "PlayerState";
  }

  // Functions forward to the states
  public virtual void Move(Vector3 direction) { }
  public virtual void Jump()                  { }

  public virtual void OnGroundCollision(Collision collision, GroundController groundController) { }

  public virtual void OnAttachableCollision(Collision collision, AttachableController attachableController) {
    attachableController.Attach(controller);
  }

  public virtual void OnDestructableCollision(Collision collision, DestructableController destructableController) {
    destructableController.Collide(collision, controller);
    controller.ExplodeAttached();
  }
}
