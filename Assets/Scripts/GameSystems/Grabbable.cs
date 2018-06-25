using UnityEngine;
/// <summary>
/// Grabbable is a base class of all objects which can be grabbed.
/// </summary>
public class Grabbable : MonoBehaviour {

	public enum DartStatus { Idle, IsGrabbed, IsThrown, IsHit }
	public Transform 	grabbingGuide;
	public Rigidbody 	rbd;

	protected DartStatus 	dartStatus = DartStatus.Idle;

	public virtual DartStatus Status {
		get{ return dartStatus;}
		set{ 
			dartStatus = value;
			if (value == DartStatus.IsGrabbed) {
				DisablePhysicsSim ();

			} else {
				EnablePhysicsSim ();
			}
		}
	}

	protected void SnapToHand(){
		if (dartStatus != DartStatus.IsGrabbed) { return; }
		if (grabbingGuide != null) {
			transform.position = grabbingGuide.position;
			transform.rotation = grabbingGuide.rotation;
		}
	}

	protected virtual void Start () {
		rbd = GetComponent<Rigidbody> ();
		EnablePhysicsSim ();
	}

	protected void EnablePhysicsSim(){
		rbd.useGravity = true;
		rbd.isKinematic = false;
	}

	protected void DisablePhysicsSim(){
		rbd.useGravity = false;
		rbd.isKinematic = true;
	}

	protected virtual void Thrown(){
		if (dartStatus != DartStatus.IsThrown) { return; }
		// get velocity from grabbing hand

	}

	protected virtual void Update () {
		SnapToHand ();
		Thrown ();
	}
}
