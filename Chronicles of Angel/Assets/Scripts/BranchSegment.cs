using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Everlie/Branch Segment", fileName="Branch Segment", order=2)]
public class BranchSegment : StorySegment {

    public AudioSequence winAudioSegment;
	public float winTimeOut;
    public AudioSequence loseAudioSegment;
	public float loseTimeOut;

    private AudioSource introAudioSource;

    [HideInInspector] public float segmentTimer;


    [HideInInspector] public bool shouldPlayWin = true;

    private GameObject graphicsObject;

    public override void Play(){
        segmentTimer = 0f;

        introAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
        introAudioSource.clip = shouldPlayWin ? winAudioSegment.masterSoundClip : loseAudioSegment.masterSoundClip;
		introAudioSource.time = startDelay;
        introAudioSource.Play ();
        introAudioSource.loop = false;

        if (shouldPlayWin)
        {
            winAudioSegment.segmentLength = winAudioSegment.masterSoundClip.length;
            graphicsObject = Instantiate(winAudioSegment.UIGraphics, GameObject.FindGameObjectWithTag("GameController").transform);
        }
        else
        {
            loseAudioSegment.segmentLength = loseAudioSegment.masterSoundClip.length;
            graphicsObject = Instantiate(loseAudioSegment.UIGraphics, GameObject.FindGameObjectWithTag("GameController").transform);
        }

        graphicsObject.GetComponent<RectTransform>().offsetMax = Toolbox.Instance.rt.offsetMax;
        graphicsObject.GetComponent<RectTransform>().offsetMin = Toolbox.Instance.rt.offsetMin;
        graphicsObject.transform.localScale = Vector3.one;
    }

    public override void Update(){
        segmentTimer += Time.deltaTime;

		if (segmentTimer >= (shouldPlayWin? winTimeOut:loseTimeOut) - 3) {
			SoundFadeMaster.FadeSound(introAudioSource, 3, true);
			OnSegmentCompleted ();
            Clear ();
        }
    }

    public override void Clear(){
        Destroy (introAudioSource);
    }

	public override void Skip(){
		SoundFadeMaster.FadeSound (introAudioSource, 3, true);
		OnSegmentCompleted ();
		Clear ();
	}
}