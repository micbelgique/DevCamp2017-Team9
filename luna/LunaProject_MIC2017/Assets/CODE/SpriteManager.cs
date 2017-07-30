using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public GameObject[] Sprite_Box;

    [HideInInspector]
    public bool CanMove = true;  

    public void ReloadSprite()
    {
        int _Length = Sprite_Box.Length;

        for (int i = 0; i < _Length; i++)
        {
            Sprite_Box[i].SetActive(false);
        }

        CanMove = true;
        Sprite_Box[Random.Range(0, _Length)].SetActive(true);


    }
}
