﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int poolSize = 10;
	public Color colorgreen;
	public Color colorred;
	public Color colorPlayer1;
	public Color colorPlayer2;
	[Header("Prefabs")]
	public GameObject eventCardPrefab;
	public GameObject timelineCardPrefab;
	[Header("References")]
	public Transform canvas;
	public Transform pool;
	public Image scoreFill;
	public Text currentPlayerText;
	public Text correctWrongText;
	public Text infoText;
	public GameObject feedbackPanel;
	public InfoPanel infoPanel;
	public GameOverPanel gameOverPanel;

	private int player1Score, player2Score = 0;
	private int currentPlayer;
	public List<string>[] failedStrings = new List<string>[] {new List<string>(), new List<string>()};

	public bool holdingCard = false;

	void Awake(){
		instance = this;
	}

	void Start () {
		holdingCard = false;
		StartMatch ();
	}

	void Update(){
		float temp = player1Score + player2Score;
		scoreFill.fillAmount = Mathf.Lerp(scoreFill.fillAmount, player1Score == 0 && player2Score == 0 ? 0.5f : player1Score / temp, Time.deltaTime * 5);
		if (Input.GetKeyDown (KeyCode.Space)) {
			print (player1Score / temp);
		}
	}

	public void StartMatch(){
		Event[] events = Resources.LoadAll<Event> ("Events");
		poolSize = Mathf.Clamp (poolSize, 2, events.Length - 1);
		foreach (Transform t in Timeline.instance.content) {
			GameObject.Destroy (t.gameObject);
		}
		foreach (Transform t in pool) {
			GameObject.Destroy (t.gameObject);
		}
		for (int i = 0; i < events.Length; i++) {
			Event temp = events [i];
			int rnd = Random.Range (0, i);
			events [i] = events [rnd];
			events [rnd] = temp;
		}
		for (int i = 0; i < poolSize; i++) {
			GameObject card = Instantiate (eventCardPrefab, pool);
			card.GetComponent<EventCard> ().SetEvent(events [i]);
		}
		GameObject tCard = Instantiate (timelineCardPrefab, Timeline.instance.content);
		Timeline.instance.AddEvent (events [poolSize]);
		tCard.GetComponent<TimelineCard> ().SetEvent(events [poolSize]);

		currentPlayer = 1;
		feedbackPanel.SetActive (false);
		currentPlayerText.color = colorPlayer1;
	}

	public void EndRound(int score){
		if (currentPlayer == 1) {
			player1Score += score;
			currentPlayer = 2;
			currentPlayerText.color = colorPlayer2;
		} else {
			player2Score += score;
			currentPlayer = 1;
			currentPlayerText.color = colorPlayer1;
		}
//		print ("SCORE | Player1: " + player1Score + " | Player2: " + player2Score);
		currentPlayerText.text = "Spelare " + currentPlayer + ":s tur!";

		if (score > 0 && pool.GetComponentsInChildren <EventCard> ().Length == 0) {
//			print ("Game Over!");
			gameOverPanel.SetWinner (player1Score, player2Score);
			gameOverPanel.gameObject.SetActive (true);
		}
			
	}

	public void ShowFeedbackPanel(bool correct, Event historyEvent){
		feedbackPanel.SetActive (true);
		if (correct) {
			correctWrongText.text = "Rätt!";
			correctWrongText.color = colorgreen;
			infoText.text = historyEvent.eventName + "\n" + historyEvent.GetDate().ToString("dd/MM/yyy");
		} else {
			correctWrongText.text = "Fel!";
			correctWrongText.color = colorred;
			infoText.text = "";
//			if (failedStrings.ContainsKey(currentPlayer)) {
//				print ("finns inte");
//				failedStrings.Add (currentPlayer, new List<string> ());
//				print ("skapades");
//			}	
//			if (!failedStrings [currentPlayer].Contains (historyEvent.eventName))
//				failedStrings [currentPlayer].Add (historyEvent.eventName);
			if (!failedStrings[currentPlayer-1].Contains(historyEvent.eventName)){
				failedStrings [currentPlayer - 1].Add (historyEvent.eventName);
			}
		}
	}

}
