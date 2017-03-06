using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sounds are loaded here so that other classes do not have to hold the memory clip
// in there memory.
public class GameSounds : MonoBehaviour {

  public AudioClip[] attaches;
  public AudioClip[] detaches;

  public AudioClip musicMain;
  public AudioClip musicGameOverWin;
  public AudioClip musicGameOverLoss;
}
