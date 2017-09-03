using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Everlie/Minigame/AcceptReject", fileName = "AcceptReject", order=1), System.Serializable]
public class AcceptReject : MiniGame {

    public enum State
	{
		Up,
		Down,
		Left,
		Right,
	}

	private State state;

	private int yesCount;
	private int noCount;
	private Vector2 startPos;
	private List<Vector2> recordedPositions = new List<Vector2>();
	private Vector2 sum;
	private Vector2 average;
	private Vector2 directionVector;

	private float timer;

	[Range(0.1f,1f)]
	public float delay = 0.5f;

    private Vector3 lastMousePos;

    public override void Reset()
    {
        yesCount = 0;
        noCount = 0;
    }

    void WaitForNextSample(){
		average = Vector2.zero;
		for (int i = 0; i < recordedPositions.Count; i++) {
			average += recordedPositions [i];
		}

		directionVector = average;

			switch (state) {
			case State.Down:
				if (Mathf.Abs (directionVector.x) > Mathf.Abs (directionVector.y)) {
					yesCount = 0;
					if (directionVector.x > 0) {
						state = State.Right;
					} else {
						state = State.Left;
					}
				} else {
					if (directionVector.y > 0) {
						yesCount++;
						state = State.Up;
					}
				}
				break;
			case State.Up:
				if (Mathf.Abs (directionVector.x) > Mathf.Abs (directionVector.y)) {
					yesCount = 0;
					if (directionVector.x > 0) {
						state = State.Right;
					} else {
						state = State.Left;
					}
				} else {
					if (directionVector.y < 0) {
						yesCount++;
						state = State.Down;
					}
				}
				break;
			case State.Left:
				if (Mathf.Abs (directionVector.y) > Mathf.Abs (directionVector.x)) {
					noCount = 0;
					if (directionVector.y > 0) {
						state = State.Up;
					} else {
						state = State.Down;
					}
				} else {
					if (directionVector.x > 0) {
						noCount++;
						state = State.Right;
					}
				}
				break;
			case State.Right:
				if (Mathf.Abs (directionVector.y) > Mathf.Abs (directionVector.x)) {
					noCount = 0;
					if (directionVector.y > 0) {
						state = State.Up;
					} else {
						state = State.Down;
					}
				} else {
					if (directionVector.x < 0) {
						noCount++;
						state = State.Left;
					}
				}
				break;	
			}
		recordedPositions.Clear ();
	}

	public override void Update ()
	{
	    ExitTestButton.AddValuesToTrack(yesCount, noCount);
	    if (Input.touchCount > 0)
	    {
	        Touch touch = Input.touches[0];
	        recordedPositions.Add(touch.deltaPosition);
	    }
	    else
	    {
	        recordedPositions.Add(new Vector2(Input.mousePosition.x, Input.mousePosition.y) -
	                              new Vector2(lastMousePos.x, lastMousePos.y));
	        lastMousePos = Input.mousePosition;
	    }

	    if (timer >= delay) {
			timer = 0f;
			WaitForNextSample ();
		} else {
			timer += Time.deltaTime;
		}

	    base.Update();

	    VerifyCompletion();
	}

    public void VerifyCompletion()
    {
        if (yesCount > 3 || noCount > 3)
        {
            End();
            CompletionCallback(yesCount - noCount);
        }
    }
}