using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

	public Text winnerText;
	public Text player1ScoreText;
	public Text player2ScoreText;
	public Text player1,player2;
	public Text p1WorkOn, p2WorkOn;

	void Start () {
		gameObject.SetActive (false);
	}

	public void SetWinner (int player1Score, int player2Score){
		if (player1Score > player2Score) {
			winnerText.text = "Spelare 1 vann!";
			winnerText.color = GameManager.instance.colorPlayer1;
		} else if (player1Score < player2Score) {
			winnerText.text = "Spelare 2 vann!";
			winnerText.color = GameManager.instance.colorPlayer2;
		} else {
			winnerText.text = "Oavgjort!";
			winnerText.color = Color.white;
		}
		player1ScoreText.text = player1Score + " poäng";
		player1.color = GameManager.instance.colorPlayer1;
		player2ScoreText.text = player2Score + " poäng";
		player2.color = GameManager.instance.colorPlayer2;

		for (int i = 0; i < 2; i++){
			string workOnText = "Träna på:";
			print (GameManager.instance.failedStrings [i].Count);
			for (int j = 0; j < (GameManager.instance.failedStrings [i].Count > 3 ? 3 : GameManager.instance.failedStrings [i].Count); j++) {
				workOnText += "\n" + GameManager.instance.failedStrings [i] [j] + ",";
			}
			if (i == 1) {
				p1WorkOn.text = workOnText;
			} else {
				p2WorkOn.text = workOnText;
			}
		}
	}
}
