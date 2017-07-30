using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaRunAndJump : MonoBehaviour
{

    public float amplitude = 10.0f;
    public float maxJumpHeight =4.0f;
    float xSin = 0;
    public float SpeedxSin = 1.0f;

	public bool jumping;
	Vector3 v3;
    float initPos;

	// Use this for initialization
	void Start ()
    {
        initPos = transform.localPosition.y;
        jumping = false;
		v3 = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (jumping && transform.localPosition.y > initPos)
        {
            ToJump ();
            //ToJumpSmooth();
		}
        else
        {
			v3.y = initPos;
			transform.localPosition = v3;
			//currentJumpSpeed = 0.0f;
			jumping = false;
		}

    }
 
	public void ToJump ()
    {
        if (!jumping)
        {
            jumping = true;
        }

        xSin += SpeedxSin * Time.deltaTime;

        float currenthigh = amplitude*(Mathf.Sin(xSin));

        currenthigh = Mathf.Min(currenthigh, maxJumpHeight);

        transform.localPosition = new Vector3(v3.x, initPos + currenthigh, v3.z);

        if (currenthigh < 0.01f && xSin > 0.5f)
        {
         //   Debug.Log("endJump");

            jumping = false;
            xSin = 0.0f;

            transform.localPosition = new Vector3(v3.x, initPos, v3.z);
        }

	}
}
