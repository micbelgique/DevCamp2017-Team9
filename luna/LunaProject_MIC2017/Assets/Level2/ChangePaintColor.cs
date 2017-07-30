using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePaintColor : MonoBehaviour {

	public GameObject image;

	Color color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

	// Use this for initialization
	void Start () {
		color = GetComponent<SpriteRenderer> ().color;
	}
	
	// Update is called once per frame

	void OnMouseDown(){
		image.GetComponent<DrawingOnTexture> ().setColor (color);
        image.GetComponent<DrawingOnTexture>().AllowToDraw();

    }

}
