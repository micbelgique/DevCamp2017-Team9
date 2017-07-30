using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMANAGER : MonoBehaviour
{
    public AudioSource Victory;
    public AudioSource BackgrSound;

    public LunaRajInteractivity Luna;
    public BckGroundManager bGroundManager;
    public HUDBAR hud;
    public DoorManager DoorManager;
    public GameObject[] Pos;
    public SpriteManager[] spriteManager;

    [Space(30)]
    public float SpeedObject = 0.5f;
    public float DespawnX = -10.0f;
    public float RespawnX = 10.0f;
    public float MinDistBetweenPOS = 0.5f;
    public float MaxDistanceToAdd = 5.0f;

    [Range (0.0f, 100.0f)]
    public float PercentForNoSpawn = 90.0f;

    float AddToMinDsitance = 0.0f;
    int _Length;
    float _DeltaTime;
    bool once = true;

    Vector3 SpawnPos;

    public float DistFrHome = 10.0f;
    float currentDistFrHome = 0.0f;

    void Start()
    {
        Victory.Stop();
        BackgrSound.Play();
        _Length = Pos.Length;
        SpawnPos = new Vector3(RespawnX, 0, 0);

        for (int i = 0; i < _Length; i++)
        {
            spriteManager[i] = Pos[i].GetComponent<SpriteManager>();
            spriteManager[i].ReloadSprite();
        }
    }

    void Update()
    {
        if (!Luna.isDead)
        {

            ScrollLevel();

            PercentLevel();
        }
    }

    void ScrollLevel()
    {
        _DeltaTime = Time.deltaTime;

        for (int i = 0; i < _Length; i++)
        {
            // Can he move?
            if (Pos[i].transform.localPosition.x > DespawnX)
            {
                if (spriteManager[i].CanMove)    //is not waiting for spawn
                    Pos[i].transform.Translate(-SpeedObject * _DeltaTime, 0, 0);
                else
                {
                    if (i == 0)
                    {
                        // checking if elligide to spawn
                        if (Pos[i].transform.localPosition.x - Pos[_Length - 1].transform.localPosition.x > (MinDistBetweenPOS + AddToMinDsitance))
                        {
                            if (((currentDistFrHome / DistFrHome) * 100) < 85.0f)
                            {
                                AddToMinDsitance = Random.Range(0.0f, MaxDistanceToAdd);
                                spriteManager[i].ReloadSprite();
                            }
                        }
                    }
                    else
                    {
                        // checking if elligide to spawn
                        if (Pos[i].transform.localPosition.x - Pos[i - 1].transform.localPosition.x > (MinDistBetweenPOS + AddToMinDsitance))
                        {
                            if (((currentDistFrHome / DistFrHome) * 100) < 85.0f)
                            {
                                AddToMinDsitance = Random.Range(0.0f, MaxDistanceToAdd);
                                spriteManager[i].ReloadSprite();
                            }

                        }
                    }
                }
            }
            else
            {
                spriteManager[i].CanMove = false;
                Pos[i].transform.localPosition = SpawnPos;
            }
        }         
    }

    void PercentLevel()
    {

        float _percent = ((currentDistFrHome / DistFrHome) * 100);

        if (currentDistFrHome < DistFrHome)
        {
            currentDistFrHome += 0.1f * _DeltaTime;
            hud.Moving(_percent);
        }

        //  print((currentDistFrHome / DistFrHome)*100);

        if (!DoorManager.bdoorfinished)
        {
            bGroundManager.ScrollBckGround(-SpeedObject);
        }
        else
        {
            Luna.Finished();
            BackgrSound.Stop();
            if (once == true && Luna.AnimLuna.GetBool("IsWalking"))
            {
                once = false;
                Victory.Play();
                Victory.loop = false;
            }
        }


        
        if (99.0f < _percent)
        {
            DoorManager.DoorMove(SpeedObject);
        }
    }
}
