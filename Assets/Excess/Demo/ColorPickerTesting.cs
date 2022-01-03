using UnityEngine.UI;
using UnityEngine;

public class ColorPickerTesting : MonoBehaviour
{
    [SerializeField] Image displayPicked, displayHover;
	[SerializeField] ColorPicker picker;

	//Display the picked color
	void Start(){picker.pickColor.AddListener((Color pickC) => {displayPicked.color = pickC; displayHover.color = Color.white;});}
	//Display the hover color
	void Update() {if(picker.arePicking) {displayHover.color = picker.hoverColor;}}
}