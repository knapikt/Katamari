using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A class for attaching to the player

public class AttachableController : StateControlledMonoBehavior<AttachableState, AttachableController> {
  
  public AttachableAttachedState AttachedState { get; private set; }
  public AttachableFreeState     FreeState     { get; private set; }

  public Rigidbody rigidBody { get; private set; }

  public GameObject DefaultParentGameObject { get; private set; }

  private GameSounds gameSounds;

  // Variables to ensure an attach doesn't happen as the object detaches
  private float lastDetachTime = float.MinValue;
  private float attachCooldown = 2.0f;

  // Having trouble keeping scale when detaching from an object. This helps set the scale back to the initial value
  private Vector3 initialScale;

  private void Awake() {
    gameObject.tag = Tag.Attachable;

    // Locate a parent object for when the Attachable is not attached
    DefaultParentGameObject = GameObject.FindGameObjectWithTag(Tag.Interactive);
    Assert.IsNotNull(DefaultParentGameObject, "Failed to locate a gameobject with tag interactive");

    // Attach a collider, set the material to something sticky
    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(ResourceConstant.Ground);

    // Attach a rigidbody
    rigidBody = gameObject.AddComponent<Rigidbody>();

    // Track initial scale so that it can be set back properly on detach
    initialScale = transform.localScale;
  }

  private void Start() {
    // Locate other components
    gameSounds = FindObjectOfType<GameSounds>();

    // Initialize states
    AttachedState = new AttachableAttachedState(this);
    FreeState     = new AttachableFreeState(this);

    // Initialize data
    State = FreeState;
  }

  public void Attach(PlayerController playerController) {
    // Do not allow quickly occuring attachments
    if (lastDetachTime + attachCooldown < Time.time) {
      playerController.Attach(this);
      State.Attach(playerController);
      SoundController.Instance.PlayRandomSound(gameSounds.attaches);
    }
  }

  public void Detach(PlayerController playerController) {
    State.Detach(playerController);
    lastDetachTime = Time.time;
    transform.localScale = initialScale;
  }
    
  public float Mass { 
    get {
      Vector3 scale = transform.localScale;
      return scale.x * scale.y * scale.z;
    }
  }
}
