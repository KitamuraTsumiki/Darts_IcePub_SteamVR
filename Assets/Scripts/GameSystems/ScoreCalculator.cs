using UnityEngine;

public class ScoreCalculator : MonoBehaviour {

	public int totalScore;

	private void OnCollisionEnter(Collision collision){
		var dart = collision.gameObject.GetComponent<Dart> ();
		if (dart != null) {
			// stop a dart
			dart.Status = Dart.DartStatus.IsHit;

			// get the position of a dart on the board
			ContactPoint contact = collision.contacts[0];
			Vector2 hitPos = new Vector2(contact.point.x, contact.point.y);

			CalculateScore (hitPos);


		}
	}

	private void CalculateScore(Vector2 hitPosition){
		// get basic info to calculate the score
		Vector2 center = new Vector2(transform.position.x, transform.position.y);
		float distFromCenter = Vector2.Distance (center, hitPosition);
		float angleInDegree = Vector2.SignedAngle (center, hitPosition);

		// thresholds


		// get the score of the hit area

		int score = 1;
		totalScore += score;
	}
}
