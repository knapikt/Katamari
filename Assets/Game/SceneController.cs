using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// In theory we have all scene loads go through our own custom class

public class SceneController : MonoBehaviorSingleton<SceneController> {

  public void LoadLevel(string level) {
    SceneManager.LoadScene(level);
  }
}
