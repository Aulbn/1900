using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimelineCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField]
	private Event historyEvent;
	public Text eventNameText;
	public Text yearText;

//	private bool isMouseOver = false;

	void Start(){
		if (historyEvent != null) {
			eventNameText.text = historyEvent.eventName;
			yearText.text = "" + historyEvent.year;
		}
	}

	void Update(){
//		if (isMouseOver) {
//			GameManager.instance.infoPanel.gameObject.SetActive (isMouseOver);
//			GameManager.instance.infoPanel.SetInfo (historyEvent.eventName, historyEvent.eventDescription);
//		} else {
//			GameManager.instance.infoPanel.gameObject.SetActive (isMouseOver);
//		}
	}

	public Event GetEvent(){
		return historyEvent;
	}

	public void SetEvent(Event historyEvent){
		this.historyEvent = historyEvent;
		eventNameText.text = historyEvent.eventName;
	}

//	public bool InsideRect(Vector3 pos){
//		RectTransform rect = GetComponent<RectTransform> ();
//		Vector3[] corners = new Vector3[4];
//		rect.GetWorldCorners (corners);
//
//		if (pos.x > corners [0].x && pos.x < corners [2].x) {
//			if (pos.y < corners [1].y && pos.y > corners [3].y)
//				return true;
//		}
//		return false;
//	}

	public void OnPointerEnter(PointerEventData pointerEventData){
//		isMouseOver = true;
		if (!GameManager.instance.holdingCard) {
			GameManager.instance.infoPanel.gameObject.SetActive (true);
			GameManager.instance.infoPanel.SetInfo (historyEvent.eventName, historyEvent.eventDescription);
		}
	}

	public void OnPointerExit(PointerEventData pointerEventData){
//		isMouseOver = false;
		if (!GameManager.instance.holdingCard)
			GameManager.instance.infoPanel.gameObject.SetActive (false);
	}

}
