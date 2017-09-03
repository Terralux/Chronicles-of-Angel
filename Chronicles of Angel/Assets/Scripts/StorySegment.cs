using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySegment : ScriptableObject {
    public virtual void Play()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Clear()
    {

    }

	public virtual void Skip()
	{
		
	}

    public delegate void VoidEvent();
	public VoidEvent OnSegmentCompleted;

	public float startDelay = 0f;

	protected IEnumerator WaitForGraphicsFade(){
		yield return new WaitForSeconds (3f);
		Clear ();
	}
}