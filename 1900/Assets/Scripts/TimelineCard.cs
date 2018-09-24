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

	void Start(){
		if (historyEvent != null) {
			eventNameText.text = historyEvent.eventName;
			yearText.text = "" + historyEvent.year;
		}
	}

	public Event GetEvent(){
		return historyEvent;
	}

	public void SetEvent(Event historyEvent){
		this.historyEvent = historyEvent;
		eventNameText.text = historyEvent.eventName;
	}

	public void OnPointerEnter(PointerEventData pointerEventData){
		if (!GameManager.instance.holdingCard) {
			GameManager.instance.infoPanel.gameObject.SetActive (true);
			GameManager.instance.infoPanel.SetInfo (historyEvent.eventName, historyEvent.eventDescription);
		}
	}

	public void OnPointerExit(PointerEventData pointerEventData){
		if (!GameManager.instance.holdingCard)
			GameManager.instance.infoPanel.gameObject.SetActive (false);
	}

}
