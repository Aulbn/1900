using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

	public Text winnerText;
	public Text scoreText;

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
		scoreText.text = "Spelare 1: " + player1Score + " poäng\nSpelare 2: " + player2Score + " poäng";
	}
}
