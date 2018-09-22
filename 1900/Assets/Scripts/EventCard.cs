﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventCard : MonoBehaviour, IDragHandler, IEndDragHandler{
	[SerializeField]
	private Event historyEvent;
	public Text eventNameText;

	private RectTransform rect;
	private Vector2 pixelScale;

	void Start(){
		if(historyEvent != null)
			eventNameText.text = historyEvent.eventName;
		rect = transform as RectTransform;			
	}

	public void OnDrag(PointerEventData eventData){
		SetPixelScale ();
		transform.parent = GameManager.instance.canvas;
		transform.position = new Vector3(Input.mousePosition.x + pixelScale.x/2, Input.mousePosition.y - pixelScale.y/2);
	}

	public void OnEndDrag (PointerEventData eventData){
		if (Timeline.instance.isMouseOver) {
			if (Timeline.instance.AddCard (historyEvent)) {
				Destroy (gameObject);
				GameManager.instance.ShowFeedbackPanel (true, historyEvent);
				GameManager.instance.EndRound(1);
			} else {
				GameManager.instance.ShowFeedbackPanel (false, historyEvent);
				GameManager.instance.EndRound(0);
			}
		}
		transform.parent = GameManager.instance.pool;
	}

	private void SetPixelScale(){
		Vector3[] corners = new Vector3[4];
		rect.GetWorldCorners (corners);
		pixelScale = new Vector2 (corners [3].x - corners [0].x, corners [1].y - corners [0].y);
	}

	public Event GetEvent(){
		return historyEvent;
	}

	public void SetEvent(Event historyEvent){
		this.historyEvent = historyEvent;
		eventNameText.text = historyEvent.eventName;
	}
}
