using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
    void Update()
    {
        if (StateMANAGER.INSTANCE.WaitTime < 0.0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StateMANAGER.INSTANCE.NextState();
            }
        }
        else
        {
            StateMANAGER.INSTANCE.WaitTime -= Time.deltaTime;
        }
    }
}
