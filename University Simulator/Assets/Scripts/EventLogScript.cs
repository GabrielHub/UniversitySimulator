using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventLogScript : MonoBehaviour
{
	private List<string> eventLog = new List<string> ();
	private string text = "";
	public TextMeshProUGUI eventLogText;

	public int maxLines;

    public void AddEvent(string eventString) {
    	eventLog.Add(eventString);
 
         if (eventLog.Count >= maxLines)
             eventLog.RemoveAt(0);
 
         text = "";
 
         foreach (string log in eventLog)
         {
             text += log;
             text += "\n\n";
         }

         eventLogText.text = text;
    }
}
