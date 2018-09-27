using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

	public Text winnerText;
	public Text player1ScoreText;
	public Text player2ScoreText;
	public Text player1,player2;

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
	}
}
