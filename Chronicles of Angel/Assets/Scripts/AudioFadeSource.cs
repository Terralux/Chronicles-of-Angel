using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeSource {

    public AudioSource audio;
    private float startVolume;

    private float fadeTime;
    private float progress;
    private bool isFadingOut;

    public AudioFadeSource(AudioSource newAudio, float fadeTime, bool isFadingOut)
    {
        audio = newAudio;
        this.fadeTime = fadeTime;
        startVolume = audio.volume;
        progress = 0f;
        this.isFadingOut = isFadingOut;
    }

    public bool EvaluateFade()
    {
		if (!audio) {
			return true;
		}

        float fraction = progress / fadeTime;
        audio.volume = startVolume * (isFadingOut ? 1f - fraction : fraction);

        if (audio.volume <= 0f && isFadingOut)
        {
            Debug.Log("I removed a fade out sound");
            return true;
        }

        if (audio.volume >= 0.999f && !isFadingOut)
        {
            Debug.Log("I removed a fade in sound");
            return true;
        }
        return false;
    }

    public bool Contains(AudioSource ac){
        if (audio != ac) {
            return false;
        } else {
            return true;
        }
    }

    public void AddProgress(float deltaTime){
        this.progress = this.progress + deltaTime;
    }
}
