using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineCard : MonoBehaviour {
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

}
