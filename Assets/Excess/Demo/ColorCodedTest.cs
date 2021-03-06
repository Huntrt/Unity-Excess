using UnityEngine;
using ExcessPackage;

public class ColorCodedTest : MonoBehaviour
{
	[SerializeField] TMPro.TextMeshProUGUI display;
	[SerializeField] Color color;

	void OnValidate()
	{
		if(display == null) {return;}
		display.text = "This " + UtilitesEx.ColorCode("word", color) + " is yellow";
	}
}
