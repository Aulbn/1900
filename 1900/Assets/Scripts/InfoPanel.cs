using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {
	[SerializeField]
	private Text titleText;
	[SerializeField]
	private Text infoText;

	void Start(){
		gameObject.SetActive (false);
	}

	public void SetInfo(string title, string info){
		titleText.text = title;
		infoText.text = info;
	}
}
