using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MultiGraphicButton : MonoBehaviour
{
	[Tooltip("The button excess component that use multi graphic")] [SerializeField] ButtonExcess excessButton;
	[Tooltip("All the target need tint")] public List<TargetTint> tints;
	//The target graphic and it color block for tint
	[System.Serializable] public class TargetTint {public Graphic targetGraphic; public ColorBlock color;}

	///When an value got change in the inspector
    void OnValidate()
	{
		//Attempt to get the button excess on this object if haven't has any excess button
		if(excessButton == null) {excessButton = GetComponent<ButtonExcess>();}
		//Print an error if this component has no ButtonExcess component assign
		if(excessButton == null) {Debug.LogError("The multitarget component of "+gameObject.name+" need an button excess component to work"); return;}
		//Reset all the target graphic color back to normal color
		UpdateColor(ButtonExcess.States.Normal);
	}

	void Start()
	{
		//Update target color when the button state change if there is excess button assign
		if(excessButton != null) {excessButton.onStateChange.AddListener(UpdateColor);}
	}

	public void UpdateColor(ButtonExcess.States state)
	{
		//Don't update the color if the button state are either holded or release
		if(state == ButtonExcess.States.Holded || state == ButtonExcess.States.Released) {return;}
		//Go through all the target graphic need to tint
		foreach (TargetTint tint in tints)
		{
			//Don't tint the color if there is no target graphic
			if(tint.targetGraphic == null) {continue;}
			//The color graphic target will tint to
			Color nextColor = Color.black;
			//Set the next color base on the current button state
			switch(state)
			{
				case ButtonExcess.States.Normal: nextColor = tint.color.normalColor; break;
				case ButtonExcess.States.Highlighted: nextColor = tint.color.highlightedColor; break;
				case ButtonExcess.States.Pressed: nextColor = tint.color.pressedColor; break;
				case ButtonExcess.States.Selected: nextColor = tint.color.selectedColor; break;
				case ButtonExcess.States.Disabled: nextColor = tint.color.disabledColor; break;
			}
			//Multiplying the next color with color multiplier
			nextColor *= tint.color.colorMultiplier;
			//Fade the current target graphic color into the next color with fade duration 
			tint.targetGraphic.CrossFadeColor(nextColor, tint.color.fadeDuration, true, true);
		}
	}
}