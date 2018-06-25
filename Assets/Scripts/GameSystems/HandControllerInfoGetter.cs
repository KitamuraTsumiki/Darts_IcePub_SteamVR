using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerInfoGetter : MonoBehaviour {

	public 		enum WhichHand { left, right }
	public 		WhichHand 	whichHand = WhichHand.left;
	protected 	Grabbing 	grabbing;

	protected virtual void Start () {
		grabbing = GetComponent<Grabbing> ();
	}
	
	protected virtual void Update () {
		
	}

	protected virtual void GetGrabbingStatus(){
		
	}

	protected virtual void AssignVelocity(){
		
	}

	protected virtual void Recenter(){
		
	}
}
