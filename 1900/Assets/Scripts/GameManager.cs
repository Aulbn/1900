using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

	void Awake(){
		instance = this;
	}

	void Start () {
		StartMatch ();
	}

//	void Update(){
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			Transform[] cards = pool.GetComponentsInChildren <Transform>();
//			cards [1].SetParent (canvas);
//			cards [1].SetParent(pool);
//			print ("Set!");
//		}
//	}

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
	}
}
