using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Everlie/Minigame/DoubleTap", fileName = "DoubleTap", order=2), System.Serializable]
public class DoubleTap : MiniGame
{
    private int clicks = 0;

    public override void Reset()
    {
        clicks = 0;
    }

	public override void Update ()
	{
	    if (Input.touchCount > 0)
	    {
	        if (Input.GetTouch(0).phase == TouchPhase.Began)
	        {
	            clicks++;
	        }
	    }
	    else
	    {
	        if (Input.GetMouseButtonDown(0))
	        {
	            clicks++;
	        }
	    }

	    base.Update();

	    VerifyCompletion();
	}

    public void VerifyCompletion()
    {
        if (clicks >= 2)
        {
            End();
            CompletionCallback(1);
        }
    }
}