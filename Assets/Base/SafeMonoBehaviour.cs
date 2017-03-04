using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SafeMonoBehaviour : MonoBehaviour {

	protected float infrequentLateUpdateDelay = 1;
	private float nextInfrequentLateUpdate = 0;

	public new void StopCoroutine(IEnumerator enumerator) {
		if (enumerator != null) {
			base.StopCoroutine(enumerator);
		}
	}

	public T AddOrGetUniqueComponent<T>() where T : Component {
		T uniqueComponent = gameObject.GetComponent<T>();

		if (uniqueComponent == null) {
			uniqueComponent = gameObject.AddComponent<T>();
		}

		return uniqueComponent;
	}

	protected virtual void LateUpdate() {
		if (Time.realtimeSinceStartup > nextInfrequentLateUpdate) {
			nextInfrequentLateUpdate = Time.realtimeSinceStartup + infrequentLateUpdateDelay;
			InfrequentLateUpdate();
		}
	}

	protected virtual void InfrequentLateUpdate() {}
}
