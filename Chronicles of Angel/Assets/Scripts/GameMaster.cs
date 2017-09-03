using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public delegate void voidEvent ();
	public voidEvent OnStorySegmentEnded;

	public CollectiveStory story;

    public GameObject background;
    private AudioSource myAudioSource;
    private bool isPlaying = false;

    public voidEvent InitiateStory;

    void Awake()
    {
        Toolbox.RegisterComponent<GameMaster> (this);
        myAudioSource = gameObject.GetComponent<AudioSource>();
        myAudioSource.Play();
        InitiateStory += BeginStory;
    }

    public void BeginStory () {
		story.Initiate();
        background.SetActive(true);
        isPlaying = true;

        SoundFadeMaster.FadeSound(myAudioSource, 3f, true);
    }

	void Update(){
	    if (isPlaying)
	    {
	        story.Update();
	    }
	}

	public void SkipCurrentSegment() {
		story.SkipCurrentSegment ();
	}
}