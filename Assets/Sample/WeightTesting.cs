using System.Collections.Generic;
using ExcessPackage;
using UnityEngine;

public class WeightTesting : MonoBehaviour
{
	public List<Utilites.ObjectWeight> objects = new List<Utilites.ObjectWeight>();

	public void LogResult()
	{
		//Log thr weight result name
		LogEx.Debug.Log(Utilites.WeightResult(objects).name);
	}
}