using UnityEngine;
/// <summary>
/// This class defines the status (grabbed or not grabbed) of a dart.
/// </summary>
public class Dart : Grabbable {

	private float hitTime;

	public override DartStatus Status {
		get {
			return base.Status;
		}
		set {
			base.Status = value;

			if (value == DartStatus.IsGrabbed || value == DartStatus.IsHit) {
				DisablePhysicsSim ();
				if (value == DartStatus.IsHit) {
					hitTime = Time.time;
				}

			} else {
				EnablePhysicsSim ();
			}
		}
	}

	protected override void Start () {
		base.Start ();
	}

	protected override void Thrown(){
		if (dartStatus != DartStatus.IsThrown) { return; }
		// modify the direction where a dart flies
		Vector3 originalVel = rbd.velocity;
		Vector3 targetVector = transform.InverseTransformDirection (originalVel);
		targetVector.x = 0f;
		targetVector = transform.TransformDirection (targetVector);
		Vector3 torque = targetVector.normalized - originalVel.normalized;

		float torqueMul = 1f;
		rbd.AddRelativeTorque (torque * torqueMul);
		float forceMul = 0.3f;
		rbd.AddForce (targetVector * forceMul);
	}

	private void StopAtDartBoard(){
		if (dartStatus != DartStatus.IsHit) { return; }
		// fix the position of a dart

		// add some vfx when a dart hit?

		// destroy the gameobject after a while
		float stayingPeriod = 1f;
		if (hitTime != 0f && Time.time > hitTime + stayingPeriod) {
			Destroy (gameObject);
		}
	}

	protected void FixedUpdate(){
		Thrown();
		StopAtDartBoard ();
	}

	protected override void Update () {
		// update motion of a dart depending on the status
		SnapToHand();

	}
}
