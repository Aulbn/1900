using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int poolSize = 10;
	public Color colorgreen;
	public Color colorred;
	[Header("Prefabs")]
	public GameObject eventCardPrefab;
	public GameObject timelineCardPrefab;
	[Header("References")]
	public Transform canvas;
	public Transform pool;
	public Image scoreFill;
	public Text currentPlayerText;

	private int player1Score, player2Score = 0;
	private int currentPlayer;


	void Awake(){
		instance = this;
	}

	void Start () {
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
	}

	public void EndRound(int score){
		if (currentPlayer == 1) {
			player1Score += score;
			currentPlayer = 2;
			currentPlayerText.color = colorred;
		} else {
			player2Score += score;
			currentPlayer = 1;
			currentPlayerText.color = colorgreen;
		}
		print ("SCORE | Player1: " + player1Score + " | Player2: " + player2Score);
		//Change texts + graphics
		currentPlayerText.text = "Spelare " + currentPlayer + ":s tur!";
	}

}
