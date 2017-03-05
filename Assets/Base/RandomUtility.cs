using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class RandomUtility {
  public static T RandomElement<T>(this IList<T> list) {
    if (list == null || list.Count == 0) {
      return default(T);
    }

    return list[Random.Range(0, list.Count)];
  }
}
