using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DialogController : MonoBehaviour {

  private static GameObject _overlayCanvas;

  private static GameObject OverlayCanvas {
    get {
      if (_overlayCanvas == null) {
        _overlayCanvas = GameObject.FindGameObjectWithTag(Tag.OverlayCanvas);
      }

      return _overlayCanvas;
    }
  }

  public void Show() {
    transform.SetParent(OverlayCanvas.transform, false);
  }
    
  public void Dismiss() {
    ObjectPoolController.Instance.PutBack(gameObject);
  }

  public static T Retrieve<T>() where T : DialogController {
    string controllerName = typeof(T).Name;
    Assert.IsTrue(controllerName.EndsWith("Controller"));

    string prefabName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

    GameObject retrievedGameObject = ObjectPoolController.Instance.Retrieve(prefabName);
    T retrievedController = retrievedGameObject.GetComponent<T>();

    Assert.IsNotNull(retrievedController, "Failed to located the controller");

    return retrievedController;
  }

}
