using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BckGroundManager : MonoBehaviour {

    public GameObject[] PosBckGroundBase;

    public float DespawnX = -10.0f;
    public float RespawnX = 10.0f;

    float _Length;
    float _DeltaTime;
    Vector3 RespawnVec;

    private void Start()
    {
        _Length = PosBckGroundBase.Length;
        RespawnVec = new Vector3(RespawnX, 0, 0);
    }

    public void ScrollBckGround(float SpeedObject)
    {
        _DeltaTime = Time.deltaTime;

        for (int i = 0; i < _Length; i++)
        {
            Transform bcktransfor = PosBckGroundBase[i].transform;
            bcktransfor.Translate(SpeedObject*_DeltaTime, 0, 0,Space.Self);

            if (bcktransfor.localPosition.x < DespawnX)
            {
                bcktransfor.localPosition = RespawnVec;
            }
        }
    }
}
