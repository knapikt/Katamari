using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AttachableController : StateControlledMonoBehavior<AttachableState, AttachableController> {
  
  public AttachableAttachedState AttachedState { get; private set; }
  public AttachableFreeState     FreeState     { get; private set; }

  public Rigidbody rigidBody { get; private set; }

  public GameObject DefaultParentGameObject { get; private set; }

  private GameSounds gameSounds;
  private float lastDetachTime = float.MinValue;
  private float attachCooldown = 2.0f;
  private Vector3 initialScale;

  private void Awake() {
    gameObject.tag = Tag.Attachable;

    // Locate a parent object for when the Attachable is not attached
    DefaultParentGameObject = GameObject.FindGameObjectWithTag(Tag.Interactive);
    Assert.IsNotNull(DefaultParentGameObject, "Failed to locate a gameobject with tag interactive");

    // Attach a collider, set the material to something sticky
    BoxCollider collider = gameObject.AddComponent<BoxCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);

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
    SoundController.Instance.PlayRandomSound(gameSounds.detaches);
  }
    
  public float Mass { 
    get {
      Vector3 scale = transform.localScale;
      return scale.x * scale.y * scale.z;
    }
  }
}
