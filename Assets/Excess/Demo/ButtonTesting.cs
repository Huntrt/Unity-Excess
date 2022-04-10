using ExcessPackage.UI;
using ExcessPackage;
using UnityEngine;

public class ButtonTesting : MonoBehaviour
{
	ButtonExcess button; 
	
	void Start()
	{
		//Get the button excess component on this object
		button = GetComponent<ButtonExcess>();
		//Log the state when it got change
		button.onStateChange.AddListener((ButtonState s) => {LogEx.Debug.Log(s.ToString());});
	}
}
