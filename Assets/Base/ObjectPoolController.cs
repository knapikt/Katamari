using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviorSingleton<ObjectPoolController> {

  public void Destroy(GameObject gameObject) {
    GameObject.Destroy(gameObject);
  }
}
