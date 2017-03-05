using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : StateControlledMonoBehavior<PlayerState, PlayerController> {

  private HashSet<AttachableController> attached = new HashSet<AttachableController>();

  // Components
  public Rigidbody rigidBody { get; private set; }

  // States
  public PlayerGroundState  GroundState  { get; private set; }
  public PlayerJumpingState JumpingState { get; private set; }

  // Private variables
  private float _health;

  // Settings
  public float PreferredMaxSpeed { get; private set; }

  // Delegates
  public delegate void FloatFloat(float v1, float v2);

  public event FloatFloat OnMassChanged = delegate {};
  public event FloatFloat OnHealthPercentChanged = delegate {};

  private void Awake() {
    gameObject.tag = Tag.Player;

    gameObject.AddComponent<PlayerInput>();

    rigidBody = gameObject.AddComponent<Rigidbody>();
    rigidBody.angularDrag        = 0;
    rigidBody.maxAngularVelocity = 10;

    SphereCollider collider = gameObject.AddComponent<SphereCollider>();
    collider.material = (UnityEngine.PhysicMaterial)Resources.Load(PhysicsMaterials.Ground);
  }

  private void Start() {
    // Instantiate states
    GroundState  = new PlayerGroundState(this);
    JumpingState = new PlayerJumpingState(this);

    // Initialize settings
    State = GroundState;
    PreferredMaxSpeed = 25.0f;
    Health = HealthMax;
    Mass = 1;
  }

  protected override void FixedUpdate() {
    base.FixedUpdate();

    // Cap the speed of the object
    if (Speed > PreferredMaxSpeed) {
      Speed = PreferredMaxSpeed;
    }
  }

  private void OnCollisionEnter(Collision collision) {
    switch(collision.gameObject.tag) {
    case Tag.Ground:
      var groundController = collision.gameObject.GetComponent<GroundController>();
      Assert.IsNotNull(groundController, "Failed to locate a ground controller");
      State.OnGroundCollision(collision, groundController);
      break;
    case Tag.Attachable:
      var attachableController = collision.gameObject.GetComponent<AttachableController>();
      Assert.IsNotNull(attachableController, "Failed to locate an attachable controller");
      State.OnAttachableCollision(collision, attachableController);
      break;
    case Tag.Destructable:
      var destructableController = collision.gameObject.GetComponent<DestructableController>();
      Assert.IsNotNull(destructableController, "Failed to locate an destructable controller");
      State.OnDestructableCollision(collision, destructableController);
      break;
    default:
      break;
    }
  }

  public void Move(Vector3 direction) {
    State.Move(direction);
  }

  public void Jump() {
    State.Jump();
  }

  public void ExplodeAttached() {
    int initialAttachCount = AttachCount;
    int toRemoveCount = (int)(0.5f * initialAttachCount);
    int count = 0;

    // Copy attachables to an array. This avoids errors when looping over a list that is changing in size
    AttachableController[] attachableList = new AttachableController[initialAttachCount]; 
    attached.CopyTo(attachableList);

    // Remove objects from the Player
    foreach (AttachableController attachableController in attachableList) {
      if (count >= toRemoveCount) { 
        break;
      }

      // Detach the object from the Player and explode the object
      attachableController.Detach(this);
      Detach(attachableController);
      attachableController.rigidBody.AddExplosionForce(100, attachableController.transform.localPosition, 3f);
    }
  }

  public void Detach(AttachableController attachableController) {
    if (attached.Contains(attachableController)) {
      attached.Remove(attachableController);
      Debug.Log(string.Format("Removing mass: {0}", attachableController.Mass));
      Mass -= attachableController.Mass;
    }
  }
    
  public void Attach(AttachableController attachableController) {
    if (!attached.Contains(attachableController)) {
      attached.Add(attachableController);
      Debug.Log(string.Format("Adding mass: {0}", attachableController.Mass));
      Mass += attachableController.Mass;
    }
  }

  // Properties ----------------------------------------------------------------------
  public bool Alive { get { return Health > 0; } }

  public int AttachCount { get { return attached.Count; } }

  public float Speed { 
    get { return rigidBody.velocity.magnitude; } 
    set {
      Vector3 v = rigidBody.velocity;
      v.Normalize();
      v *= value;
      rigidBody.velocity = v;
    }
  }

  public float Health    {
    get {
      return Mathf.Clamp(_health, 0, HealthMax);
    }

    set {
      float initialHealthPercent = HealthPercent;
      _health = Mathf.Clamp(value, 0, HealthMax);
      OnHealthPercentChanged(initialHealthPercent, HealthPercent);
    }
  }

  public float HealthMax { get { return 100f; } } 

  public float HealthPercent {
    get {
      return Health / HealthMax;
    }
  }

  public float Mass {
    get { return rigidBody.mass;  }
    set {
      float initialValue = Mass;
      rigidBody.mass = value; 
      transform.localScale = Vector3.one * 2 * Mathf.Pow(Mass * 0.75f * Mathf.PI, 0.33333f);
      OnMassChanged(initialValue, Mass);
    }
  }

}
