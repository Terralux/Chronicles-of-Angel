using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSequence {
	public AudioClip masterSoundClip;
	public GameObject UIGraphics;

	[HideInInspector]
	public float segmentLength;

	[Range(0f,3f)]
	public float fadeOutTime;
}