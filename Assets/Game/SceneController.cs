﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviorSingleton<SceneController> {

  public void LoadLevel(string level) {
    SceneManager.LoadScene(level);
  }
}
