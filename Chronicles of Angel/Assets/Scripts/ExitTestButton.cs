using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExitTestButton : MonoBehaviour
{
    private static int yesCount = 0;
    private static int noCount = 0;

    public static void AddValuesToTrack(int yes, int no)
    {
        yesCount = yes;
        noCount = no;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "QUIT BUILD"))
        {
            Application.Quit();
        }

        GUI.Box(new Rect(10, 50, 100, 30), yesCount.ToString());
        GUI.Box(new Rect(10, 85, 100, 30), noCount.ToString());

		if (GUI.Button (new Rect (10, 120, 100, 30), "Skip segment")) {
			Toolbox.FindRequiredComponent<GameMaster> ().SkipCurrentSegment ();
		}
    }
}
