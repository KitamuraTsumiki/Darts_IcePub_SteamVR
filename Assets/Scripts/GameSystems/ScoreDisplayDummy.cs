using UnityEngine;

public class ScoreDisplayDummy : MonoBehaviour {

	public ScoreCalculator scoreCalculator;
	private TextMesh textMesh;

	void Start () {
		textMesh = GetComponent<TextMesh> ();
	}
	
	void Update () {
		string textToDisplay = "Score: " + scoreCalculator.totalScore.ToString();
		textMesh.text = textToDisplay;
	}
}
