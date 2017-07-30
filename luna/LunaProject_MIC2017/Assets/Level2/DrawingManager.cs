using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour {

	public GameObject imageToPaintOn;
	// Use this for initialization

	public void QuitDrawing(){
		Debug.Log ("Quit drawing (drawing manager)");
		imageToPaintOn.GetComponent<DrawingOnTexture> ().NoMoreDrawing ();
	}
}
