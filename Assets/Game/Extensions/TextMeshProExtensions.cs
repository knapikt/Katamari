using UnityEngine;
using System;
using System.Collections;

namespace TMPro {

  public partial class TextMeshProUGUI {

    public string formatText;

    private float lerpDuration = 1f;

    public void LerpToTargetValue(int startValue, int targetValue, string format = "{0}") {
      StopAllCoroutines();
      StartCoroutine(CountUp(startValue, targetValue));
    }

    private IEnumerator CountUp(int startValue, int targetValue, string format = "{0}") {
      int currentValue = int.MaxValue;
      float startLerpTime = Time.time;
      float endLerpTime = startLerpTime + lerpDuration;
      float t = 0;

      while (t <= 1) {
        t = (Time.time - startLerpTime) / (endLerpTime - startLerpTime);

        int nextValue = (int)Mathf.Lerp(startValue, targetValue, t);
        if (nextValue != currentValue) {
          currentValue = nextValue;

          if (string.IsNullOrEmpty(formatText)) {
            text = string.Format(format, currentValue);
          } else {
            try {
              text = string.Format(formatText, currentValue);
            } catch (Exception e) {
              Debug.Log(e);
              Debug.LogFormat("AnimatingTextMeshProUGUI: formatText needs a {0}");
            }
          }
        }

        yield return null;
      }
    }
  }
}
