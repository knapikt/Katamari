﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviorSingleton<ObjectPoolController> {

  public GameObject Retrieve(string objectName, Vector3 position) {
    return (GameObject)Instantiate(Resources.Load(objectName), position, Quaternion.identity);
  }

  public GameObject Retrieve(string objectName) {
    return Retrieve(objectName, Vector3.zero);
  }

  public void PutBack(GameObject gameObject) {
    GameObject.Destroy(gameObject);
  }
}
