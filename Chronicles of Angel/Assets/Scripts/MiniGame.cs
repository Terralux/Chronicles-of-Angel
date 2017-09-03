using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : StorySegment {
	public AudioClip interactionLoopSound;

	public AudioClip idleSpeak1;
	public AudioClip idleSpeak2;

	public GameObject UIGraphics;

	private AudioSource interactionLoopAudioSource;
    private AudioSource idleSpeakAudioSource;

	private GameObject graphicsObject;

	private float idleSpeakTimer = 0f;
	public float idleSpeakDelay = 30f;

	private bool hasPlayedIdleSpeak = false;
	private bool hasMoreSoundsToPlay = true;

    public delegate void intEvent(int completionScore);
    public intEvent CompletionCallback;

	public void Initialize(){
		idleSpeakTimer = 0f;
		hasPlayedIdleSpeak = false;
	    hasMoreSoundsToPlay = true;

		interactionLoopAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
	    interactionLoopAudioSource.volume = 1f;
		interactionLoopAudioSource.clip = interactionLoopSound;
		interactionLoopAudioSource.Play();
		interactionLoopAudioSource.loop = true;
	    SoundFadeMaster.FadeSound(interactionLoopAudioSource, 0.5f, false);

	    idleSpeakAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
	    interactionLoopAudioSource.volume = 1f;
	    idleSpeakAudioSource.clip = idleSpeak1;
	    idleSpeakAudioSource.Stop ();
	    idleSpeakAudioSource.loop = false;
	    SoundFadeMaster.FadeSound(idleSpeakAudioSource, 0.5f, false);
	}

	public override void Play(){
	    Initialize();

	    graphicsObject = Instantiate(UIGraphics, GameObject.FindGameObjectWithTag("GameController").transform);
	    graphicsObject.GetComponent<RectTransform>().offsetMax = Toolbox.Instance.rt.offsetMax;
	    graphicsObject.GetComponent<RectTransform>().offsetMin = Toolbox.Instance.rt.offsetMin;
	    graphicsObject.transform.localScale = Vector3.one;
	    interactionLoopAudioSource.Play();
	}

	public override void Update (){

	    idleSpeakTimer += Time.deltaTime;

		if (idleSpeakTimer > idleSpeakDelay && hasMoreSoundsToPlay) {
			if (hasPlayedIdleSpeak) {
			    idleSpeakAudioSource.clip = idleSpeak2;
			    idleSpeakAudioSource.Play ();
				hasMoreSoundsToPlay = false;
			} else {
			    idleSpeakAudioSource.clip = idleSpeak1;
			    idleSpeakAudioSource.Play ();
				hasPlayedIdleSpeak = true;
				idleSpeakTimer = 0f;
			}
		}
	}

	public override void Clear(){
		Destroy (interactionLoopAudioSource);
		Destroy (idleSpeakAudioSource);
		Destroy (graphicsObject);
	}

    public virtual void Reset()
    {

    }

	public virtual void Skip(){
		End ();
		CompletionCallback (1);
	}

    public void End()
    {
		//Handheld.Vibrate ();
        SoundFadeMaster.FadeSound(interactionLoopAudioSource, 3, true);
        SoundFadeMaster.FadeSound(idleSpeakAudioSource, 3, true);
		Toolbox.FindRequiredComponent<GameMaster> ().StartCoroutine (WaitForGraphicsFade ());
    }
}