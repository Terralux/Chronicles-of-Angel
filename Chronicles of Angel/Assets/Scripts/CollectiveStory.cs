using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Everlie/Collective Story", fileName="Collective Story", order=0)]
public class CollectiveStory : ScriptableObject {

	public List<StorySegment> story = new List<StorySegment>();

	private int currentIndex = 0;

	public void Initiate()
	{
	    currentIndex = 0;
		story [currentIndex].Play ();
		story [currentIndex].OnSegmentCompleted += OnReceivedFade;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	public void Update(){
		story [currentIndex].Update ();
	}

	public void OnReceivedFade(){
		if (story.Count > currentIndex)
		{
		    Debug.Log("Complete Segment");
			story[currentIndex].OnSegmentCompleted -= OnReceivedFade;
			story [currentIndex + 1].Play ();

		    if (story[currentIndex + 1] as MiniGame != null)
		    {
				SleepMode.SetInteraction(true);

		        (story[currentIndex + 1] as MiniGame).CompletionCallback += OnReceivedCompletion;
		        (story[currentIndex + 1] as MiniGame).Reset();
		        Debug.Log("Next segment is a Minigame");
		    }
		    else
		    {
				SleepMode.SetInteraction(false);

		        story[currentIndex + 1].OnSegmentCompleted += OnReceivedFade;
		        Debug.Log("Next segment is an AudioSequence");
		    }

		    currentIndex++;
		}
	}

    public void OnReceivedCompletion(int score)
    {
        Debug.Log("Received a Completion");
		(story[currentIndex] as MiniGame).CompletionCallback -= OnReceivedCompletion;

		SleepMode.SetInteraction(false);

        if (story.Count > currentIndex)
        {
            if (story[currentIndex + 1] as BranchSegment != null)
            {
                (story[currentIndex + 1] as BranchSegment).shouldPlayWin = score > 0;
            }

            story[currentIndex + 1].OnSegmentCompleted += OnReceivedFade;
            story[currentIndex + 1].Play();
            currentIndex++;
        }
    }

	public void SkipCurrentSegment(){
		story [currentIndex].Skip ();
	}
}