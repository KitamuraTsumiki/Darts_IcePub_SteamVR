using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVR_InfoGetter : HandControllerInfoGetter {


	private Vector3 lastVel;

	protected override void Start () {
		base.Start ();
	}

	protected override void Update () {
		GetGrabbingStatus ();
		AssignVelocity ();
	}

	protected override void GetGrabbingStatus(){
		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);
		grabbing.isGrabbing = device.GetPress(SteamVR_Controller.ButtonMask.Trigger);
	}

	protected override void AssignVelocity(){
		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);
		Vector3 currVel = device.velocity;
		grabbing.handVelocity = currVel;
		grabbing.handAcceleration = (currVel - lastVel).magnitude;
		lastVel = currVel;
	}

	private void ActivateHaptics(){
		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
			device.TriggerHapticPulse ();
		}
	}
	protected override void Recenter(){

	}
}
