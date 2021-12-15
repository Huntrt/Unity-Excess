using System.Collections.Generic;
using UnityEngine;

namespace ExcessPackage
{
	public struct Utilites
	{
		#region Color Coded
		public static string ColorCode(string text, Color color)
		{
			//Get the hex of the color has assign
			string colorString = ColorUtility.ToHtmlStringRGB(color);
			//Return the text with color coded value
			return "<color=#" + colorString + ">" + text + "</color>";
		}
		#endregion

		#region Weight System
		//The item will drop and it weight to drop
		public class WeightItem {public Object item ; public float weight;}

		//Drop an item inside an list parameter by using it weight
		public static Object Dropping(List<WeightItem> list)
		{
			//Getting the total weight of all the item in list
			float total = 0; foreach (WeightItem w in list) {total += w.weight;}
			//Generate chance from 0 to total weight
			float chance = Random.Range(0, total);
			//Go through all the item in list
			for (int d = list.Count - 1; d >= 0 ; d--)
			{
				//Drop the item if it weight take all the chance
				if((chance - list[d].weight) <= 0) {return list[d].item;}
				//Decrease the chance with item weight if it FAIL to take all the chance
				else {chance -= list[d].weight;}
			}
			//Send null only when somehow it go below 0
			return null;
		}
		#endregion
	}
}