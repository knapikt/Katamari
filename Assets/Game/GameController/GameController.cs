using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the lifecyle of the game

public class GameController : StateControlledMonoBehavior<GameState, GameController> {

  public int attachableCount   = 300;
  public int destructableCount = 300;

  // Components used by states
  public GameSounds       GameSounds       { get; private set; }
  public PlayerController PlayerController { get; private set; } 

  // States
  public GameOnState   GameOn   { get; private set; }
  public GameOverState GameOver { get; private set; }

  // Win conditions
  public int AttachableQuota { get; private set; }

  // Environment Spawning
  private int spanRadius = 150;

  private void Awake() {
    AttachableQuota = 20;
  }

	private void Start() {
    // Locate components
    GameSounds       = FindObjectOfType<GameSounds>();
    PlayerController = FindObjectOfType<PlayerController>();

    // Adjust physics
    Physics.gravity = new Vector3(0, -40.0F, 0);

    // Define states
    GameOn   = new GameOnState(this);
    GameOver = new GameOverState(this);

    // Initialize GameController
    State = GameOn;

    GenerateObstacles();
	}

  private void GenerateObstacles() {
    // Create randomly sized and located attachables
    for (int i = 0; i < attachableCount; ++i) {
      Vector3 randomPosition = Vector3.zero;
      Vector2 location = Random.insideUnitCircle * spanRadius;
      randomPosition.x = location.x;
      randomPosition.z = location.y;
      randomPosition.y = 10f;

      float size = Random.Range(1, 4);

      GameObject attachableObject = ObjectPoolController.Instance.Retrieve(ResourceConstant.Collectable, randomPosition);
      attachableObject.transform.localScale = new Vector3(size, size, size);
    }

    // Create randomly sized and located destructables
    for (int i = 0; i < destructableCount; ++i) {
      Vector3 randomPosition = Vector3.zero;
      Vector2 location = Random.insideUnitCircle * spanRadius;
      randomPosition.x = location.x;
      randomPosition.z = location.y;
      randomPosition.y = 10f;

      float size = Random.Range(1, 4);

      GameObject destructableObject = ObjectPoolController.Instance.Retrieve(ResourceConstant.Destructable, randomPosition);
      destructableObject.transform.localScale = new Vector3(size, size, size);

      DestructableController controller = destructableObject.GetComponent<DestructableController>();
      controller.damage = size * 5;
      controller.explosionForce = size * 10;
      controller.explosionRadius = size * 10;
    }
  }
}
