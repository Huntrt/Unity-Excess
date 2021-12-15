using UnityEngine;

namespace Huntrt.Excess
{
	public struct Utilites
	{
		public static string ColorCode(string text, Color color)
		{
			//Get the hex of the color has assign
			string colorString = ColorUtility.ToHtmlStringRGB(color);
			//Return the text with color coded value
			return "<color=#" + colorString + ">" + text + "</color>";
		}
	}
}