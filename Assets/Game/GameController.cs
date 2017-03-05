using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


  private GameSounds gameSounds;

	private void Start() {
    gameSounds = FindObjectOfType<GameSounds>();


    Physics.gravity = new Vector3(0, -40.0F, 0);
    SoundController.Instance.PlayMusicAudioClip(gameSounds.musicMain);
	}
	
}
