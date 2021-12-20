using System.Collections.Generic;
using ExcessPackage.UI;
using ExcessPackage;
using UnityEngine;
using TMPro;

public class WeightTesting : MonoBehaviour
{
	[SerializeField] ButtonExcess excessButton;
	public List<UtilitesEx.ObjectWeight> drops = new List<UtilitesEx.ObjectWeight>();
	public List<TextMeshProUGUI> counters = new List<TextMeshProUGUI>();

	void Update() {if(excessButton.currentState == ButtonState.Holded) {CountResult();}}

	public void CountResult()
	{
		//The name of object has drop
		string drop = UtilitesEx.WeightResult(drops).name + " counter";
		//Increase the counter of the object has drop
		foreach (TextMeshProUGUI c in counters) {if(drop == c.name) {c.text = (int.Parse(c.text) + 1).ToString();}}
	}
}