using UnityEngine;
using System.Collections;

// http://redframe-game.com/blog/global-managers-with-generic-singletons/

public class MonoBehaviorSingleton<T> : SafeMonoBehaviour where T : Component {

	private static T instance;
	private static string singletonsParentName = "SingletonsParent";
	
	public static T Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<T>();
				if (instance == null) {
					GameObject gameObject = new GameObject();
					gameObject.hideFlags = HideFlags.DontSave;
					gameObject.name = typeof(T).Name;
					instance = gameObject.AddComponent<T>();

					gameObject.transform.SetParent(ParentGameObject.transform);
				}
			}

			return instance;
		}
	}

	public void Load() { }

	public virtual void Awake() {
		DontDestroyOnLoad(gameObject);
		if (instance == null) {
			instance = this as T;
		} else {
			Debug.Log("Potentially serious problem with Singletons. Multiple instance being created. Scene may be corrupted: " + this.GetType());
			Destroy(gameObject);
		}
	}

	private void OnApplicationFocus(bool focusStatus) {
		if (Instance != this) {
      Destroy(gameObject);
		}
	}

	private void OnApplicationQuit() {
		GameObject[] parentGameObjects = GameObject.FindGameObjectsWithTag(singletonsParentName);
		foreach (GameObject parentGameObject in parentGameObjects) {
      Destroy(parentGameObject);
		}
	
    Destroy(gameObject);
	}

	private static GameObject _parentGameObject;
	public static GameObject ParentGameObject {
		get {
			_parentGameObject = GameObject.Find(singletonsParentName);
			if (_parentGameObject == null) {
				_parentGameObject = new GameObject();
				_parentGameObject.name = singletonsParentName;
				_parentGameObject.tag = singletonsParentName;
				_parentGameObject.hideFlags = HideFlags.DontSave;
				DontDestroyOnLoad(_parentGameObject);
			}
			return _parentGameObject;
		}
	}
}
