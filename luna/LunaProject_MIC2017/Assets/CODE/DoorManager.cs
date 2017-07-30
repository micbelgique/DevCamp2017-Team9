using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public float xBase;
    public float XEnd;
    public bool bdoorfinished = false;

    private void Start()
    {
        transform.position = new Vector3(xBase, transform.position.y, transform.position.z);
        bdoorfinished = false;
    }

    public void DoorMove(float Speed)
    {
        if (transform.localPosition.x > XEnd)
            transform.Translate(-Speed * Time.deltaTime, 0, 0, Space.Self);
        else
            bdoorfinished = true;
    }
}
