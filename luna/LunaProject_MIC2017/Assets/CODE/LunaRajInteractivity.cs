using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaRajInteractivity : MonoBehaviour {

    [Range(0.01f, 1.0f)]
    public float jumpScreenPortion;


    bool finished = false;
    [HideInInspector]
    public Animator AnimLuna;
    AudioSource AudioSLuna;

    public bool isDead;
    public float invincibilityTime = 2.0f;
    float currInvincibility = 0.0f;

    public float timeRespawn = 2.0f;
    float currTimeRespawn = 0.0f;

    public float frequency = 0.9f;
    float curfrequency = 0.0f;

    public SpriteRenderer[] SpriteRend;
    LunaRunAndJump LunaRunJump = null;

    //float WaitTime = 2.0f;
    float _deltaTime;


    // Use this for initialization
    void Start ()
    {
       // WaitTime = 2.0f;
        isDead = false;
        LunaRunJump = GetComponent<LunaRunAndJump>();
        AnimLuna = this.GetComponentInChildren<Animator>();
        AudioSLuna = GetComponent<AudioSource>();
    }

    public void Finished()
    {
        finished = true;
        AnimLuna.SetTrigger("Victory");
        for (int j = 0; j < SpriteRend.Length; j++)
        {
            SpriteRend[j].color = new Color(1, 1, 1, 1);
        }

        currInvincibility = 9.0f;
        curfrequency = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        _deltaTime = Time.deltaTime;
      
        if (finished)
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
                StateMANAGER.INSTANCE.WaitTime -= _deltaTime;
            }
        }
            //Si bug, verifier que screen.height est bien comprit dans le contete. Voir pour le remplacer par screen.width
            if (!isDead && Input.GetMouseButtonDown(0) && (Screen.height * jumpScreenPortion) >= Input.mousePosition.y && finished == false)
            {
                LunaRunJump.ToJump();
            }

            if (isDead)
            {

                currTimeRespawn += 1.0f * _deltaTime;
                if (currTimeRespawn > timeRespawn)
                {
                    currInvincibility = 0.0f;

                    isDead = false;
                }
            }
            else
            {
                if (!LunaRunJump.jumping)
                    AnimLuna.SetBool("IsWalking", true);
            }

            if (LunaRunJump.jumping)
            {
                AnimLuna.SetTrigger("Jump");
                AnimLuna.SetBool("IsWalking", false);

            }


            if (currInvincibility < invincibilityTime)
            {
                currInvincibility += 1.0f * _deltaTime;

                curfrequency += 1.0f * _deltaTime;
                if (curfrequency > frequency)
                {
                    curfrequency = 0.0f;
                    for (int j = 0; j < SpriteRend.Length; j++)
                    {
                        if (SpriteRend[j].color.a == 1)
                            SpriteRend[j].color = new Color(1, 1, 1, 0);
                        else
                            SpriteRend[j].color = new Color(1, 1, 1, 1);
                    }
                }

                if (currInvincibility >= invincibilityTime)
                {
                    for (int j = 0; j < SpriteRend.Length; j++)
                    {
                        SpriteRend[j].color = new Color(1, 1, 1, 1);
                    }
                }
            }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && (currInvincibility > invincibilityTime) && finished == false)
        {
            if (collision.tag == "Obstacle")
            {
                isDead = true;
                AnimLuna.SetBool("IsWalking", false);
                AnimLuna.SetTrigger("Fall");
                currTimeRespawn = 0.0f;
                AudioSLuna.Play();
            }
        }
    }
}
