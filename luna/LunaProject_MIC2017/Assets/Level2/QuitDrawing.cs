using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitDrawing : MonoBehaviour {
	public GameObject image;
	public GameObject board;
    public DrawingOnTexture TextureManager;
	public float speed;

	bool showBoard;
	Vector3 camP;
	Vector3 drawingFuturePosition;

	void Start(){
		showBoard = false;
		camP = Camera.main.transform.position;
		camP.z = board.transform.position.z;
	}

	void Update()
    {
        if (showBoard)
        {
            board.transform.position = Vector3.Lerp(board.transform.position, camP, speed);

            if (StateMANAGER.INSTANCE.WaitTime < 0.0f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TextureManager.ResetTexture(TextureManager.tex);
                    StateMANAGER.INSTANCE.NextState();
                }
            }
            else
            {
                StateMANAGER.INSTANCE.WaitTime -= Time.deltaTime;
            }
        }

	}

	void OnMouseDown(){
		Debug.Log ("Quit Drawing");
		image.GetComponent<DrawingOnTexture> ().NoMoreDrawing();
		showBoard = true;
		//board.GetComponent<ShowBoard> ().Go ();
	}
}
