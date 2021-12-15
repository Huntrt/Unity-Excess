using System.Collections.Generic;
using UnityEngine;
using System;

namespace ExcessPackage
{
	public static class Utilites
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
		//The gameoject will drop and it weight
		[Serializable] public class ObjectWeight {public GameObject dropObject; public float weight;}

		//Drop an object in the list parameter by using it weight
		public static GameObject WeightResult(List<ObjectWeight> list)
		{
			//Getting the total weight of all the object in list
			float total = 0; foreach (ObjectWeight w in list) {total += w.weight;}
			//Generate chance from 0 to total weight
			float chance = UnityEngine.Random.Range(0, total);
			//Go through all the object in list
			for (int d = list.Count - 1; d >= 0 ; d--)
			{
				//This object are the result if it weight take all the chance
				if((chance - list[d].weight) <= 0) {return list[d].dropObject;}
				//Decrease the chance with object weight if it FAIL to take all the chance
				else {chance -= list[d].weight;}
			}
			return null;
		}
		#endregion
	}
}