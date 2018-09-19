using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Timeline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {
	public static Timeline instance;

	public Transform content;
	private List<Event> timeline = new List<Event>();
	[SerializeField]
	private GameObject timelineCardPrefab;
	public bool isMouseOver = false;
	public Transform fillObject;

	public Event tempEvent;

	void Awake(){
		instance = this;
	}

	void Start(){

	}

	void Update(){
		if (isMouseOver) {
			SetFillObject ();
		} else {
			if (fillObject.parent != GameManager.instance.canvas)
				fillObject.SetParent (GameManager.instance.canvas);
		}
	}

	private void SetFillObject(){ //VARFÖR DEN INTE FUNKA!?!?
		Timeline[] cards = content.GetComponentsInChildren <Timeline> ();
//		int index = 1;
		print ("AH");
		foreach (Timeline t in cards) {
			print (t.name + ", " + t.transform.position.x);
		}
	}

	public bool AddCard(Event historyEvent){
		int index = CorrectPlace (historyEvent);
		if (index > timeline.Count) {
			timeline.Add(historyEvent);
		} else if (index >= 0) {
			timeline.Insert (index, historyEvent);
		} else {
			print ("WRONG!");
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
		TimelineCard[] timelineCards = content.GetComponentsInChildren <TimelineCard>();
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
		isMouseOver = true;
	}

	public void OnPointerExit(PointerEventData pointerEventData){
		isMouseOver = false;
	}

}
