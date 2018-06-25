using UnityEngine;

public class Grabbing : MonoBehaviour {

	public 	bool 		isGrabbing = false; // isGrabbing is assigned by the utility of hand controller
	public 	Vector3 	handVelocity;
	public 	float 		handAcceleration;
	private Transform 	grabbingGuide;
	private Grabbable 	grabbable;

	private void Start () {
		grabbingGuide = transform.GetChild (0);
	}

	private void OnTriggerStay (Collider other){
		if (other.CompareTag("Grabbable")){
			Grabbable grabbable = other.GetComponent<Grabbable> ();
			Grab (grabbable);

		}
	}

	private void Grab(Grabbable _grabbable){
		if (grabbable != null || !isGrabbing) { return; }
		grabbable = _grabbable;
		grabbable.Status = Grabbable.DartStatus.IsGrabbed;
		if (grabbable.grabbingGuide == null) {
			grabbable.grabbingGuide = grabbingGuide;
		}
	}

	private void Release(){
		if (grabbable == null || isGrabbing) { return; }

		float accTh = 0.3f;

		if (handAcceleration > accTh) {
			grabbable.Status = Grabbable.DartStatus.IsThrown;
			grabbable.rbd.velocity = handVelocity;

		} else {
			grabbable.Status = Grabbable.DartStatus.Idle;
		}

		if (grabbable.grabbingGuide != null) {
			grabbable.grabbingGuide = null;
		}
		grabbable = null;
	}

	private void Update(){
		Release ();
	}
}
