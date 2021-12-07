using System.Collections; using UnityEngine;

/* How to use:
	- Create an log using TMPro UI by using GameLogs.debug.log();
*/

public class GameLogs : MonoBehaviour
{
	//Turn this script into singleton
	public static GameLogs debug; void Awake() {debug = this;}
	//An text to display
	[SerializeField][TextArea] public string text;
	//Log duration
	[SerializeField] float duration;
	//UI to display log
	public TMPro.TextMeshProUGUI display;

	void Start()
	{
		//If this object has an TMP Text to display
		if(GetComponent<TMPro.TextMeshProUGUI>() != null)
		{	
			//Get the TMP Text component
			display = GetComponent<TMPro.TextMeshProUGUI>();
		}
		//Send an error if this object don't has TMP Text to display
		else {Debug.LogError("Please insert an TextMeshProUGUI for GameLog to display");}
	}

	public void log(object info)
	{
		//Adding text to log
		text += info + "\n";
		//Start remove log with an set duration
		StartCoroutine("RemoveLog");
	}

	IEnumerator RemoveLog()
	{
		//Wait for an set duration
		yield return new WaitForSeconds(duration);
		//Get the index where new line (\n) are made
		int index = text.IndexOf("\n");
		//Delete the line below the new line
		text = text.Substring(index+1);
	}

	//Update log UGUI
	void Update() {display.text = text;}
}