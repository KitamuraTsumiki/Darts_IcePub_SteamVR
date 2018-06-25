using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawner : MonoBehaviour {

	public GameObject dart;

	private void Update () {
		// if there's no dart in the scene, generate a new one
		var dartInScene = FindObjectOfType<Dart> ();
		if (dartInScene == null) {
			Instantiate (dart, transform.position, Quaternion.identity);
		}
	}
}
