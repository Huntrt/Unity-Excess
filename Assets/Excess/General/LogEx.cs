using UnityEngine;

namespace ExcessPackage
{
public class LogEx : MonoBehaviour
{
	//Turn this script into singleton
	public static LogEx Debug; void Awake() {Debug = this;}
	//Current log data
	[SerializeField][TextArea] string logData;
	//The line limit and line count
	[SerializeField] int lineLimit; int lineCount;
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
		else {UnityEngine.Debug.LogError("The Log Excess don't has any TextUGUI");}
	}

	public void Log(object info)
	{
		//Adding info to log text
		logData += info + "\n";
		//Increase one line
		lineCount++;
		//Start remove old log after go out of line limit
		if(lineCount > lineLimit) {RemoveLog();}
		UpdateLog();
	}

	void RemoveLog()
	{
		//Delete the first line
		logData = logData.Substring(logData.IndexOf("\n")+1);
		//Decrease one line
		lineCount--;
		UpdateLog();
	}

	public void ClearLog()
	{
		//Remove all the log data
		logData = "";
		//Remove all the line count
		lineCount -= lineCount;
		UpdateLog();
	}

	//Update log text UI
	void UpdateLog() {display.text = logData;}
}
}