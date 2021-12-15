using System.Collections.Generic;
using UnityEngine;


namespace ExcessPackage
{
public class ObjectCycling : MonoBehaviour
{
	//List of gameobject to cycle
    public List<GameObject> cycleList = new List<GameObject>();
	public int currentCycle;

	//Display the current cycle
	void Start() {DisplayCycle();}

	public void NextCycle()
	{
		//Go to the next current cycle 
		currentCycle++;
		//Reset back to the first cycle if has go through all of cyle
		if(currentCycle >= cycleList.Count) {currentCycle = 0;}
		//Display the current cycle
		DisplayCycle();
	}

	public void BackCycle()
	{
		//Go back to the current cycle 
		currentCycle--;
		//Reset back to the final cycle if has go back all of cyle
		if(currentCycle < 0) {currentCycle = cycleList.Count-1;}
		//Display the current cycle
		DisplayCycle();
	}

	public void DisplayCycle()
	{
		//Deactive all the object in cycle list
		foreach (GameObject c in cycleList) {c.SetActive(false);}
		//Display only the current cycle object
		cycleList[currentCycle].SetActive(true);
	}
}
}
