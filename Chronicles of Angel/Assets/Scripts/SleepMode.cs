using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepMode : MonoBehaviour {

	private static float touchTimer = 0f;
	public float sleepDelay = 30f;

	static bool isWaitingForInteraction = false;
	
	void Update () {
		if (isWaitingForInteraction) {
			touchTimer += Time.deltaTime;

			if (Input.touchCount > 0) {
				touchTimer = 0f;
			}

			if (touchTimer > sleepDelay) {
				Screen.sleepTimeout = SleepTimeout.SystemSetting;
			}
		}
	}

	public static void SetInteraction(bool shouldWaitForInteraction){
		isWaitingForInteraction = shouldWaitForInteraction;
		touchTimer = 0f;
	}
}