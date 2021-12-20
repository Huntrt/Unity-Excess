using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace ExcessPackage { namespace UI 
{
public class ButtonConfirmation : MonoBehaviour
{
	[Tooltip("The button excess component that use multi graphic")] [SerializeField] ButtonExcess excessButton;
	[SerializeField] Image confirmBar; [SerializeField] float confirmDuration; float confirmCounter;
	public bool areConfirming, allowConfirm = true; public UnityEvent onConfirming;

	///When an value got change in the inspector
    void OnValidate()
	{
		//Attempt to get the button excess on this object if haven't has any excess button
		if(excessButton == null) {excessButton = GetComponent<ButtonExcess>();}
	}

	void Start()
	{
		//Print an error if this component has no ButtonExcess component assign
		if(excessButton == null) {Debug.LogError("The button confirmation component of "+gameObject.name+" need an button excess component to work"); return;}
		//Reset confirm when button excess get release while currently confirming and allow to confirm
		excessButton.onStateChange.AddListener((ButtonState s)=>{if(s == ButtonState.Released && areConfirming && allowConfirm) {ResetConfirming();}});		
	}

	void Update()
	{
		//If the confirm button currently holding while able to confirm
		if(allowConfirm && excessButton.currentState == ButtonState.Holded)
		{
			//Are currently confirming
			areConfirming = true;
			//Increase confirm counter overtime
			confirmCounter += Time.deltaTime;
			//Fill the confirm bar with the ratio between counter an duration
			BarFilling(confirmCounter / confirmDuration);
			//Send complete confirmation event and reset the progress when counter reached duration
			if(confirmCounter >= confirmDuration){ResetConfirming(); onConfirming.Invoke();}
		}
	}

	//Reset bar filling and the counter and confirm status
	void ResetConfirming() {BarFilling(0); confirmCounter -= confirmCounter; areConfirming = false;}

	//Filling the confirm progress bar with set amount
	void BarFilling(float amount) {confirmBar.fillAmount = amount;}
}
}}
