using UnityEngine;
using Huntrt.Excess;

public class ColorCodedTest : MonoBehaviour
{
	[SerializeField] TMPro.TextMeshProUGUI display;
	[SerializeField] Color color;

	void OnValidate()
	{
		if(display == null) {return;}
		display.text = "This " + Utilites.ColorCode("word", color) + " is yellow";
	}
}
