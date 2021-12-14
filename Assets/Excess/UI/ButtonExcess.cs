using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ButtonExcess : Selectable
{
	//An replicate of the default button on click event
	public Button.ButtonClickedEvent onClick;
	[Tooltip("The event when button state change")] public StateChanging onStateChange = new StateChanging();
	[Tooltip("The button's current state")] public States currentState; 
	States defaultState; //The state where the button will revert back to
	[Serializable] public class StateChanging : UnityEvent<States> {} //The event that send state
	//All the state of button
	public enum States {Normal, Highlighted, Pressed, Selected, Disabled, Holded, Released}

	///When the button state are change
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		//Run the Selectable's DoStateTransition function
		base.DoStateTransition(state, instant);
		//Check the state of transitioning
		switch(state)
		{
			//The default state are now normal when transition to normal
			case SelectionState.Normal: defaultState = States.Normal; break;
			//The default state are now highlighted when transition to highlighted
			case SelectionState.Highlighted: defaultState = States.Highlighted; break;
			//The default state are now selected when transition to selected
			case SelectionState.Selected: defaultState = States.Selected; break;
		}
		//Don't change state if the current state are holding
		if(currentState == States.Holded) {return;}
		//Send the onCLick event then Begin holding if transition state are pressed
		if(state == SelectionState.Pressed) {onClick.Invoke(); StartCoroutine(Holding(true));}
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
		//Wait for an frame to cycle between holding or not
		yield return null; isHold = !!isHold;
		//Change state to holded if currently hold and back to default state if stop holding
		if(isHold) {UpdateState("Holded");} else {UpdateState(defaultState.ToString());}
	}

	///Updating the button excess state
	void UpdateState(string StateSet)
	{
		//Update the current state to the state has set
		currentState = (States)Enum.Parse(typeof(States), StateSet.ToString());
		//Send an event with the current state
		onStateChange.Invoke(currentState);
	}
}