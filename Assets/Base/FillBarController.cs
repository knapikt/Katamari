using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FillBarController : MonoBehaviour {

  public Image foregroundFill;

  protected float percentFilled = 1.0f;
  protected float lastDisplayedPercentFilled = 1.0f;
  protected Coroutine animationCoroutine;
  protected float originalWidth = 0;

  public delegate void VoidSignature();
  public event VoidSignature UpdateComplete = delegate {};

  private void Awake() {
    originalWidth = foregroundFill.rectTransform.sizeDelta.x;
  }

  public virtual void SetPercentFilled(float value, bool animated = true, float moveDuration = 0.5f) {
    // Do not update if not active
    if (!gameObject.activeInHierarchy) {
      return;
    }

    // Sanitize input
    percentFilled = Mathf.Clamp(value, 0.0f, 1.0f);

    // Halt coroutine if one was already active
    if (animationCoroutine != null) {
      StopCoroutine(animationCoroutine);
    }

    // Either start a coroutine for animation or simply set the fill
    if (animated) {
      animationCoroutine = StartCoroutine(AnimateForegroundFill(moveDuration));
    } else {
      SetBothFills(percentFilled);
    }
  }

  protected virtual IEnumerator AnimateForegroundFill(float moveDuration) {
    float moveTime = 0f;
    float previousFilledPercent = lastDisplayedPercentFilled;

    // Loop for the moveDuration. Lerp towards the desired fill amount
    while (moveTime < moveDuration) {
      float progress = moveTime / moveDuration;
      SetBothFills(Mathf.Lerp(previousFilledPercent, percentFilled, progress));
      moveTime += Time.deltaTime;
      yield return null;
    }

    SetBothFills(percentFilled);
    UpdateComplete();
  }

  private void SetBothFills(float percentFilled_) {
    lastDisplayedPercentFilled = percentFilled_;
    foregroundFill.fillAmount  = percentFilled_;
  }
}
