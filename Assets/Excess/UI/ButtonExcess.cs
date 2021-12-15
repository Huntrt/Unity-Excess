using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace ExcessPackage { namespace UI 
{
//All the state of button
public enum ButtonState {Normal, Highlighted, Pressed, Selected, Disabled, Holded, Released}

public class ButtonExcess : Selectable
{
	//An replicate of the default button on click event
	public Button.ButtonClickedEvent onClick;
	[Tooltip("The event when button state change")] public StateChanging onStateChange = new StateChanging();
	[Tooltip("The event when button toggle change")] public Toggling onToggle = new Toggling();
	[Tooltip("The button's current state")] public ButtonState currentState; public bool areToggle;
	ButtonState defaultState; //The state where the button will revert back to
	[Serializable] public class StateChanging : UnityEvent<ButtonState> {} //The event that send state
	[Serializable] public class Toggling : UnityEvent<bool> {} //The event that toggle state

	///When the button state are change
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		//Run the Selectable's DoStateTransition function
		base.DoStateTransition(state, instant);
		//Check the state of transitioning
		switch(state)
		{
			//The default state are now normal when transition to normal
			case SelectionState.Normal: defaultState = ButtonState.Normal; break;
			//The default state are now highlighted when transition to highlighted
			case SelectionState.Highlighted: defaultState = ButtonState.Highlighted; break;
			//The default state are now selected when transition to selected
			case SelectionState.Selected: defaultState = ButtonState.Selected; break;
		}
		//Don't change state if the current state are holding
		if(currentState == ButtonState.Holded) {return;}
		//If transition state to pressed
		if(state == SelectionState.Pressed) 
		{
			//Cycle between toggle
			areToggle = !areToggle;
			//Send the toggle event
			onToggle.Invoke(areToggle);
			//Send the onCLick event
			onClick.Invoke(); 
			//Begin holding
			StartCoroutine(Holding(true));
		}
		//Update the state as the selectable transition state
		UpdateState(state.ToString());
	}

	///When no longer press the button
	public override void OnPointerUp(PointerEventData eventData)
	{
		//Run the Selectable's OnPointerUp function
		base.OnPointerUp(eventData);
		//Revert state back to normal
		StartCoroutine(Holding(false));
		//Change the state to release
		UpdateState("Released");
	}

	///Begin holding the button
	IEnumerator Holding(bool isHold)
	{
		//Wait for an frame
		yield return null;
		//Change state to holded if currently hold and back to default state if stop holding
		if(isHold) {UpdateState("Holded");} else {UpdateState(defaultState.ToString());}
	}

	///Updating the button excess state
	void UpdateState(string StateSet)
	{
		//Update the current state to the state has set
		currentState = (ButtonState)Enum.Parse(typeof(ButtonState), StateSet.ToString());
		//Send an event with the current state
		onStateChange.Invoke(currentState);
	}
}
}}