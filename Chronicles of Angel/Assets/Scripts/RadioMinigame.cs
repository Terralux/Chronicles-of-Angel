using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Everlie/Minigame/Radio", fileName = "Radio", order=1), System.Serializable]
public class RadioMinigame : MiniGame {

	public float targetValue;
	private float currentValue;
	private float valueAdjustment = 0.5f;

	public float valueThreshold;

	private Vector2 lastMousePosition;

	public override void Reset()
	{
		currentValue = 0f;
	}

	public override void Update ()
	{
		ExitTestButton.AddValuesToTrack((int)currentValue, (int)targetValue);

		if (Input.touchCount > 0) {
			switch (Input.GetTouch (0).phase) {
			case TouchPhase.Began:
				currentValue += Input.GetTouch (0).deltaPosition.x * valueAdjustment;
				break;
			case TouchPhase.Moved:
				currentValue += Input.GetTouch (0).deltaPosition.x * valueAdjustment;
				break;
			}
		} else {
			if (Input.GetMouseButton (0)) {
				currentValue += Input.mousePosition.x - lastMousePosition.x;
				lastMousePosition = Input.mousePosition;
			} else {
				lastMousePosition = Input.mousePosition;
			}
		}

		base.Update();
		VerifyCompletion();
	}

	public void VerifyCompletion()
	{
		if ((currentValue >= targetValue - valueThreshold && currentValue <= targetValue + valueThreshold))
		{
			End();
			CompletionCallback ((int)currentValue);
		}
	}
}