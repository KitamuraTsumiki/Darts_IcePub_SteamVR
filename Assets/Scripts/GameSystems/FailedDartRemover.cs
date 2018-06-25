using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedDartRemover : MonoBehaviour {

	private float hitTime;
	private Dart dart;

	private void OnCollisionEnter(Collision collision){
		dart = collision.gameObject.GetComponent<Dart> ();
		hitTime = Time.time;
	}

	private void Update(){
		float stayingPeriod = 1f;
		if (dart != null && hitTime != 0f && Time.time > hitTime + stayingPeriod) {
			// remove a dart
			Destroy(dart.gameObject);
		}
	}
}
