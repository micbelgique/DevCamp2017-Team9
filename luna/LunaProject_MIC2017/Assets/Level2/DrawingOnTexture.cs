using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingOnTexture : MonoBehaviour {

	public Texture2D tex;
	//public int XX = 00;
	public int penThickness;
	public GameObject topRight;
	public GameObject bottomLeft;

	//public Camera cam;

	bool allowedToDraw;
	Texture2D texBackUp;
	Color currentColor;
	Vector2 trPosition;
	Vector2 blPosition;

	void Start(){
		allowedToDraw = false;
		texBackUp = CopyTexture(tex);
		currentColor = new Color (1.0f, 0.0f, 0.0f);
		trPosition = topRight.transform.position;
		blPosition = bottomLeft.transform.position;
	}

	void Update()
	{
		//Texture2D tex = tex;
		//GetComponent<SpriteRenderer>().sprite.   
		//texture = tex;
		if (Input.GetButton("Fire1") && allowedToDraw) 
		{

			//int x = (int)(Input.mousePosition.x / (Screen.width / tex.width));
			//int y = (int)(Input.mousePosition.y / (Screen.height / tex.height));

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			//if (hit.collider == null) {
			//	Debug.Log ("Ne touche rien");
			//}

            if (hit.collider != null && hit.collider.gameObject.tag == "Draw")
            {
                float imageHeight = trPosition.y - blPosition.y;
                float imageWidth = trPosition.x - blPosition.x;

                float cursorRelativeY = hit.point.y - blPosition.y;
                float cursorRelativeX = hit.point.x - blPosition.x;

                int y = (int)(tex.height * (cursorRelativeY / imageHeight));
                int x = (int)(tex.width * (cursorRelativeX / imageWidth));

                //Debug.Log ("X = " + x + ", Y = " + y);

                ToDraw(tex, x, y);
            }

  
		}
		//GetComponent<SpriteRenderer>().sprite = Sprite.Create (tex, GetComponent<SpriteRenderer>().sprite.rect, new Vector2 (0.5f, 0.5f));
	}

	Texture2D CopyTexture(Texture2D textureToCopy){
		Texture2D texture = new Texture2D (textureToCopy.width, textureToCopy.height);
		for (int x = 0; x < textureToCopy.width; x++) {
			for (int y = 0; y < textureToCopy.height; y++) {
				texture.SetPixel (x, y, textureToCopy.GetPixel (x, y));
			}
		}
		texture.Apply();
		return texture;
	}

	public void ResetTexture(Texture2D textureToReset)
    {
        Debug.LogWarning("<color=yellow> TextureReseted </color> ");

		for (int x = 0; x < texBackUp.width; x++) {
			for (int y = 0; y < texBackUp.height; y++) {
				textureToReset.SetPixel (x, y, texBackUp.GetPixel (x, y));
			}
		}
		textureToReset.Apply();
	}

	void ToDraw(Texture2D textureToDrawOn, int x, int y)
    {
		Color black = new Color (0.032f, 0.016f, 0.032f, 1000);
		for(int i = x - penThickness; i <= x + penThickness; i++){
			for(int j = y - penThickness; j <= y + penThickness; j++){
				//Debug.Log ("X = " + i + ", Y =" + j);
				if (Vector2.Distance (new Vector2 (x, y), new Vector2 (i, j)) < penThickness) {
					//Debug.Log (textureToDrawOn.GetPixel(i, j));
					if(textureToDrawOn.GetPixel(i,j).r > black.r || textureToDrawOn.GetPixel(i,j).b > black.b || textureToDrawOn.GetPixel(i,j).g > black.g)
					textureToDrawOn.SetPixel (i, j, currentColor);
				}
			}
		}
		textureToDrawOn.Apply ();
	}

	public void setColor (Color newColor){
		currentColor = newColor;
	}

	public void NoMoreDrawing(){
		allowedToDraw = false;
	}

	void OnApplicationQuit()
    {
		ResetTexture (tex);
	}

	float FindRealLength(float min, float max){
		return max - min;
	}

    public void AllowToDraw()
    {
        allowedToDraw = true;
    }

}
