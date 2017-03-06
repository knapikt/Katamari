using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A state for attachables

public class AttachableState : ComponentState<AttachableController> {

  public AttachableState(AttachableController controller) : base(controller) {
    stateName = "AttachableState";
  }

  public virtual void Attach(PlayerController playerController) { }
  public virtual void Detach(PlayerController playerController) { }

}
