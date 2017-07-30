using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth_Script : MonoBehaviour {
	public GameObject VetEte;
	public GameObject VetPluie;
	public GameObject VetHiver;
	private int victoryMessage;


	// Hide all clothes sprite except the Pyjamas.
	void Start () {
		if (this.gameObject.tag == "Pyjamas") {
			this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		} else {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}

	}
	
	// Depending on the victory condition 
  // (i.e. on the matching between the current weather and the choice made 
  // by the player), the victoryMessage value is computed (from VetXXX GameObject)
  // and the right set of clothes is loaded and displayed (all other are hidden).
	void Update () {
		
		victoryMessage = VetEte.GetComponent<ClickTouch> ().victoryMessage
			+ VetPluie.GetComponent<ClickTouch> ().victoryMessage 
			+ VetHiver.GetComponent<ClickTouch> ().victoryMessage;
		
		if (victoryMessage == 1)
		{
			if (this.gameObject.tag == "Summer") {
				this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			} else {
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}

		}
		if (victoryMessage == 2) {
			if (this.gameObject.tag == "Rain") {
				this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			} else {
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
		if (victoryMessage == 3) {
			if (this.gameObject.tag == "Winter") {
				this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			} else {
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		}

	}
}
