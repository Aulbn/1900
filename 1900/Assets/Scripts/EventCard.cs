using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventCard : MonoBehaviour, IDragHandler, IEndDragHandler{
	[SerializeField]
	private Event historyEvent;
	public Text eventNameText;

	private RectTransform rect;

	void Start(){
		if(historyEvent != null)
			eventNameText.text = historyEvent.eventName;
		rect = transform as RectTransform;
	}

	public void OnDrag(PointerEventData eventData){
		transform.parent = GameManager.instance.canvas;
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag (PointerEventData eventData){
		if (Timeline.instance.isMouseOver) {
			if (Timeline.instance.AddCard (historyEvent)) {
				Destroy (gameObject);
			}
		}
		transform.parent = GameManager.instance.pool;
	}



	public Event GetEvent(){
		return historyEvent;
	}

	public void SetEvent(Event historyEvent){
		this.historyEvent = historyEvent;
		eventNameText.text = historyEvent.eventName;
	}
}
