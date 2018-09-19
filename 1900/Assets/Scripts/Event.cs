using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Event", order = 1)]
public class Event : ScriptableObject {
	public string eventName;
	public int year;
	[TextArea(5,10)]
	public string eventDescription;
}
