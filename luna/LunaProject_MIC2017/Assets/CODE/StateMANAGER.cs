using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMANAGER : MonoBehaviour
{
    public static StateMANAGER INSTANCE;
    public GameObject[] ListScene;
    int State;
    int lenght;

    [HideInInspector]
    public float WaitTime = 2.0f;

    public float TimeBetweenclick = 2.0f;

    // Use this for initialization
    void Start()
    {
        INSTANCE = this;

        State = 0;

        WaitTime = 2.0f;

        lenght = ListScene.Length;

        for (int i = 0; i < lenght; i++)
        {
            ListScene[i].SetActive(false);
        }
        ListScene[State].SetActive(true);
    }

    public void NextState()
    {
        WaitTime = TimeBetweenclick;

        ListScene[State].SetActive(false);

        State += 1;

        if (State > lenght - 1)
        {
            Debug.LogWarning("<color=yellow>State Out Of Range </color>=> GameRestarted");
            State = 0;
            SceneManager.LoadScene(0);
        }

        ListScene[State].SetActive(true);

        //switch (State)
        //{
        //    case 0:
        //        {
        //            break;
        //        }
        //    case 1:
        //        {
        //            break;
        //        }
        //    case 2:
        //        {
        //            break;
        //        }
        //    case 3:
        //        {
        //            break;
        //        }
        //    case 4:
        //        {
        //            break;

        //        }
        //}
    }
}
