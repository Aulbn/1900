using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event", menuName = "Event", order = 1)]
public class Event : ScriptableObject {
	public string eventName;
	public int year;
	[Range(1,12)]
	public int month = 1;
	[Range(1,31)]
	public int day = 1;
	[TextArea(5,10)]
	public string eventDescription;


	public System.DateTime GetDate(){
		return new System.DateTime(year, month, day);
	}
}
