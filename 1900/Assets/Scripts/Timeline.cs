using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timeline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {
	public static Timeline instance;

	public Transform content;
	private List<Event> timeline = new List<Event>();
	[SerializeField]
	private GameObject timelineCardPrefab;
	public bool isMouseOver = false;
	private float cardSpacing;

	private TimelineCard[] timelineCards;

	public Transform marker;

	void Awake(){
		instance = this;
	}

	void Start(){
		cardSpacing = content.GetComponent<HorizontalLayoutGroup> ().spacing;
	}

	void Update(){
		isMouseOver = InsideRect (Input.mousePosition);
		if (isMouseOver && GameManager.instance.holdingCard) {
			timelineCards = content.GetComponentsInChildren <TimelineCard> ();
			marker.gameObject.SetActive (true);
			UpdateMarker ();
		} else {
			marker.gameObject.SetActive (false);
		}
	}

	private void UpdateMarker(){
		Vector3[] corners = new Vector3[4];
		timelineCards[0].GetComponent<RectTransform> ().GetWorldCorners (corners);
		float cardWidth = corners [3].x - corners [0].x;

		for (int i = 0; i < timelineCards.Length; i++){
			Transform tempCard = timelineCards [i].transform;
			if (Input.mousePosition.x < tempCard.position.x) {
				marker.position = new Vector3 (tempCard.position.x - (cardWidth / 2 + cardSpacing / 2), content.position.y);
				return;
			}
		}
		//Längst åt höger
		marker.position = new Vector3 (timelineCards [timelineCards.Length-1].transform.position.x + cardWidth / 2 + cardSpacing / 2, content.position.y);
	}

	public bool AddCard(Event historyEvent){
		int index = CorrectPlace (historyEvent);
		if (index > timeline.Count) {
			timeline.Add(historyEvent);
		} else if (index >= 0) {
			timeline.Insert (index, historyEvent);
		} else {
			return false;
		}
		ReloadTimeline ();
		return true;
	}

	public void AddEvent(Event historyEvent){
		timeline.Add (historyEvent);
	}

	private void ReloadTimeline(){
		ClearTimeline ();
		LoadTimeline ();
	}

	public void LoadTimeline(){
		foreach (Event e in timeline){
			GameObject card = Instantiate (timelineCardPrefab, content);
			card.GetComponent<TimelineCard> ().SetEvent(e);
		}
	}

	public void ClearTimeline(){
		foreach (Transform child in content) {
			GameObject.Destroy (child.gameObject);
		}
	}

	private int CorrectPlace(Event historyEvent){
//		TimelineCard[] timelineCards = content.GetComponentsInChildren <TimelineCard>();
		for (int i = 0; i < timelineCards.Length; i++) {
			Vector2 tcPos = timelineCards [i].transform.position;
			if (Input.mousePosition.x < tcPos.x) {
				if (historyEvent.year < timelineCards [i].GetEvent().year) {
					if (i > 0) {
						if (historyEvent.year > timelineCards [i - 1].GetEvent().year) {
							return i;
						}
					} else
						return i;
				} else
					return -1;
			}
		}
		//Never to the left
		if (historyEvent.year > timelineCards[timelineCards.Length-1].GetEvent().year){
			return timelineCards.Length;
		}
		return -1;
	}

	public bool InsideRect(Vector3 pos){
		RectTransform rect = GetComponent<RectTransform> ();
		Vector3[] corners = new Vector3[4];
		rect.GetWorldCorners (corners);

		if (pos.x > corners [0].x && pos.x < corners [2].x) {
			if (pos.y < corners [1].y && pos.y > corners [3].y)
				return true;
		}

		return false;
	}

	public void OnPointerEnter(PointerEventData pointerEventData){
//		isMouseOver = true;
	}

	public void OnPointerExit(PointerEventData pointerEventData){
//		isMouseOver = false;
	}

}
