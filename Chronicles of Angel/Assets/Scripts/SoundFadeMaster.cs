using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeMaster : Singleton<SoundFadeMaster> {

	protected SoundFadeMaster()
	{
		// Since the Constructor is protected an instance can't be made of the Toolbox
	}

    private static List<AudioFadeSource> fadeSources = new List<AudioFadeSource>();

    public static void FadeSound(AudioSource audio, float fadeTime, bool isFadingOut)
    {
        if (fadeSources.Count >= 1) {
            for (int i = 0; i < fadeSources.Count; i++) {
                if (!fadeSources[i].Contains (audio))
                {
                    AudioFadeSource newAudioSource = new AudioFadeSource(audio, fadeTime, isFadingOut);
                    fadeSources.Add (newAudioSource);
                }
            }
        } else {
            AudioFadeSource newAudioSource = new AudioFadeSource (audio, fadeTime, isFadingOut);
            fadeSources.Add (newAudioSource);
        }
    }

	public void Update(){
		if (fadeSources != null) {
			if (fadeSources.Count > 0) {
			    List<AudioFadeSource> sourcesToRemove = new List<AudioFadeSource>();
				foreach (AudioFadeSource afs in fadeSources) {
					afs.AddProgress(Time.deltaTime);
				    if (afs.EvaluateFade())
				    {
				        sourcesToRemove.Add(afs);
				    }
				}

			    foreach (AudioFadeSource str in sourcesToRemove)
			    {
					if (str.audio) {
						if (str.audio.volume <= 0.1f) {
							Destroy (str.audio);
						}
					}

			        fadeSources.Remove(str);
			    }
			}
		}
	}
}