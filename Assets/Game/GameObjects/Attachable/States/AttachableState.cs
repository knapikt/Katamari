using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableState : ComponentState<AttachableController> {

  public AttachableState(AttachableController controller) : base(controller) {
    stateName = "AttachableState";
  }

  public virtual void Attach(PlayerController playerController) { }
}
