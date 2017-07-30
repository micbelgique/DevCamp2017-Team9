using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBAR : MonoBehaviour
{
    public float BaseX;
    public float EndX;
    public GameObject head;
    Transform headTrans;

    Vector3 Basic;

    float unit;
    private void Start()
    {
        headTrans = head.transform;
        Basic = new Vector3(BaseX, headTrans.localPosition.y, headTrans.localPosition.z);
        unit = (Mathf.Abs(EndX) + Mathf.Abs(BaseX))/100;
        headTrans.localPosition = Basic;
    }

    public void Moving(float Percent)
    {

        float pos = unit * Percent;
        headTrans.localPosition = new Vector3(Basic.x + (pos), Basic.y, Basic.z);
    }
}
