using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class SoundController : MonoBehaviorSingleton<SoundController> {

	private AudioSource soundSource;
	private AudioSource musicSource;
	private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
	private IEnumerator soundCoroutine;
	private float _musicVolume = 1;

	private bool soundSourceEnabled = true;
	private bool musicSourceEnabled = true;

	public override void Awake() {
		base.Awake();
		soundSource = gameObject.AddComponent<AudioSource>();
		musicSource = gameObject.AddComponent<AudioSource>();
	
    SoundEnabled = true;
    MusicEnabled = true;
	}

  public void PlaySound(string sound, float volumeScale = 1f) {
    PlayAudioClipImmediately(Clip(sound), volumeScale);
  }
 
	private void PlayAudioClipImmediately(AudioClip audioClip, float volumeScale = 1f) {
		if (audioClip == null) {
			return;
		}
		soundSource.PlayOneShot(audioClip, volumeScale);
	}

	public void PlayMusic(string sound, float volumeScale = 1f) {
		PlayMusicAudioClip(Clip(sound), volumeScale);
	}
	
	public void PlayMusicAudioClip(AudioClip audioClip, float volumeScale = 1f) { 
		PlayMusicAudioClip(audioClip, true, volumeScale);
	}

	private void PlayMusicAudioClip(AudioClip audioClip, bool looping, float volumeScale = 1f) {
		if (audioClip == null) {
			return;
		}
		
		musicSource.loop = looping;
		if (musicSource.clip != audioClip) {
			musicSource.clip = audioClip;
			musicSource.Play();
		}
		MusicVolume = volumeScale;
	}
    
	public bool ClipAvailable(string sound) {
		Preload(sound);
		return Clip(sound) != null;
	}

	private AudioClip Clip(string sound) {
		Preload(sound);
		if (audioClips.ContainsKey(sound)) {
			return audioClips[sound];
		}
		return null;
	}

	public void StopMusic() {
		musicSource.Stop();
	}

	public void Preload(string sound) {
		if (!string.IsNullOrEmpty(sound) && !audioClips.ContainsKey(sound)) {
			audioClips[sound] = Resources.Load(sound) as AudioClip;
		}
	}

	public bool PlayingMusic { get { return musicSource.isPlaying; } }

	public bool SoundEnabled {
		get { return soundSourceEnabled; }
		set {
			soundSourceEnabled = value;
			if (SoundEnabled) {
				soundSource.volume = 1;
			} else {
				soundSource.volume = 0;
			}
		}
	}

	public bool MusicEnabled {
		get { return musicSourceEnabled; }
		set {
			musicSourceEnabled = value;
			if (MusicEnabled) {
				musicSource.volume = _musicVolume;
			} else {
				musicSource.volume = 0;
			}
		}
	}

	public float MusicVolume {
		get { return _musicVolume; } 
		protected set { 
			_musicVolume = value; 

			if (MusicEnabled) {
				musicSource.volume = _musicVolume;
			}
		} 
	}
}
