using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventCard : MonoBehaviour {
	[SerializeField]
	private Event historyEvent;
	public Text eventNameText;

	private RectTransform rect;
	private Vector2 pixelScale;
	private bool isHeld;

	void Start(){
		if(historyEvent != null)
			eventNameText.text = historyEvent.eventName;
		rect = transform as RectTransform;			
	}

	void Update(){
		if (isHeld) {
//			print (historyEvent.GetDate ().ToString("dd/MM/yyyy"));
			SetPixelScale ();
			transform.parent = GameManager.instance.canvas;
			transform.position = new Vector3(Input.mousePosition.x + pixelScale.x/2, Input.mousePosition.y - pixelScale.y/2);

			if (Input.GetButtonDown ("Fire1") && Timeline.instance.isMouseOver) {
				if (Timeline.instance.AddCard (historyEvent)) {
					Destroy (gameObject);
					GameManager.instance.ShowFeedbackPanel (true, historyEvent);
					GameManager.instance.EndRound (1);
				} else {
					GameManager.instance.ShowFeedbackPanel (false, historyEvent);
					GameManager.instance.EndRound (0);
				}
				ReleaseCard();
			}
		}
	}

	private void ReleaseCard(){
		if (isHeld) {
			isHeld = false;
			GameManager.instance.infoPanel.gameObject.SetActive(false);
			GameManager.instance.holdingCard = false;
			GameManager.instance.infoPanel.SetInfo ("", "");
			transform.parent = GameManager.instance.pool;
		}
	}
		
	public void PickupCard(){
		if (!isHeld) {
			isHeld = true;
			GameManager.instance.infoPanel.gameObject.SetActive(true);
			GameManager.instance.holdingCard = true;
			GameManager.instance.infoPanel.SetInfo (historyEvent.eventName, historyEvent.eventDescription);
		}
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
