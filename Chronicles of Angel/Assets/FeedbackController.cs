using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour {

	public GameObject feedbackObject;

	[Range(0.1f,5f)]
	public float spawnFrequency;

	private float lastSpawn;
	private bool isTrackingOneFinger = false;

	void Update () {
		if (Input.touchCount > 0) {
			lastSpawn += Time.deltaTime;

			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				isTrackingOneFinger = true;
				SpawnFeedbackObject ();
			} else {
				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					isTrackingOneFinger = false;
					lastSpawn = 0f;
				} else {
					if (lastSpawn >= spawnFrequency) {
						SpawnFeedbackObject ();
					}
				}
			}
		} else {
			isTrackingOneFinger = false;
		}

		if (Input.GetMouseButtonDown (0)) {
			isTrackingOneFinger = true;
			SpawnFeedbackObject ();
		}

		if (Input.GetMouseButton (0)) {
			lastSpawn += Time.deltaTime;
			if (lastSpawn >= spawnFrequency) {
				SpawnFeedbackObject ();
			}
		}
	}

	private void SpawnFeedbackObject(){
		lastSpawn = 0f;
		GameObject go = Instantiate (feedbackObject, transform.parent);

		if (Input.touchCount > 0) {
			go.transform.position = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
		} else {
			go.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}

		go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y, transform.position.z);
		go.transform.localScale = Vector3.one;

		Destroy (go, 2f);
	}
}