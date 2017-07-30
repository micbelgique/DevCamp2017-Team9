using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickTouch : MonoBehaviour {

	private GameObject vetement;

	public GameObject GameController;
	public GameObject echec;
	public GameObject victoire;

  bool finished = false;

	private int idMeteo;

	public int victoryMessage;

  // At start, the "finished" condition is false and the victoryMessage indicates 
  // that the victory conditions have not been met. 
	void Start (){
        finished = false;
        victoryMessage = 0;
	}

  // If the player clicks/touches the collider of the right clothes for the current weather, the 
  // corresponding victoryMessage value is chosen (to be sent to "Cloth_Script"),
  // an arrow/cross is displayed to indicate victory/failure. In case of victory, 
  // the "finish" bool is set to true. In case of failure, 
  // the cross is displayed for 2s then diseappears.
	IEnumerator OnMouseUp ()
	{
    // Get current weather ID (1: summer, 2: rain, 3: snow)
		idMeteo = GameController.GetComponent<GameController> ().idMeteo;

		if ((idMeteo == 1) & (this.gameObject.tag == "vetété"))
        {
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            victoire.SetActive (true);
			echec.SetActive (false);
			victoryMessage = 1;
            finished = true;
        } else if ((idMeteo == 2) & (this.gameObject.tag == "vetpluie"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			victoire.SetActive (true);
			echec.SetActive (false);
			victoryMessage = 2;
            finished = true;
        } else if ((idMeteo == 3) & (this.gameObject.tag == "vethiver"))
        {
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            victoire.SetActive (true);
			echec.SetActive (false);
			victoryMessage = 3;
            finished = true;
        } else
        {
			echec.SetActive (true);
			yield return new WaitForSeconds(2f);
			echec.SetActive (false);
			victoryMessage = 0;

        }

	
	}
  
    // Check (via "finished" bool) that the winning conditions are met, then
    // load next state (panel) in StateManager.   
    void Update()
    {

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
                StateMANAGER.INSTANCE.WaitTime -= Time.deltaTime;
            }
        }
    }
}