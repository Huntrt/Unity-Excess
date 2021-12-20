using System.Collections.Generic;
using ExcessPackage;
using UnityEngine;

public class WeightTesting : MonoBehaviour
{
	public List<UtilitesEx.ObjectWeight> objects = new List<UtilitesEx.ObjectWeight>();

	public void DisplayResult()
	{
		//Log thr weight result name
		LogEx.Debug.Log(UtilitesEx.WeightResult(objects).name);
	}
}