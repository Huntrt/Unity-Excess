using System.Collections.Generic; 
using UnityEngine;

namespace ExcessPackage
{
public class Pool : MonoBehaviour
{
	//Make the pool component into singleton
	public static Pool s; void Awake() {s = this;}
	//The pool contain all the object has create
	public List<GameObject> poolList = new List<GameObject>();

	//Create the object needed with wanted position, rotation, does it auto active upon create? and do it need to has parent?
    public GameObject Create(GameObject Need, Vector3 Position, Quaternion Rotation, bool autoActive = true, Transform parent = null)
    {
		///If there is unactive object in pool
        if(poolList.Count > 0)
		{
			//Go through all the object in pool in reverse order
			for (int i = poolList.Count-1; i >= 0; i--)
			{
				//Remove any null object left from pool then skip it from being check
				if(poolList[i] == null) {poolList.RemoveAt(i); continue;}
				//Remove the "(Clone)" out of gameobject from pool name
				string objectName = poolList[i].name.Replace("(Clone)","");
				//If there is an unactive object in pool with the same name of object need to get
				if(!poolList[i].activeInHierarchy && string.Equals(Need.name, objectName))
				{
					//Set the polled object position
					poolList[i].transform.position = Position;
					//Set the polled object rotation
					poolList[i].transform.rotation = Rotation;
					//Set the pooled object parent from parameter
					poolList[i].transform.SetParent(parent);
					//Active it depend if need to active
					poolList[i].SetActive(autoActive);
					//Return it that object and no need to continue code
					return poolList[i];
				}
			}
		}
		///If there is no unactive object left in pool
		{
			//Create the needed object witth set position and rotation
			GameObject newObject = Instantiate(Need, Position, Rotation);
			//Set the new object parent from parameter
			newObject.transform.SetParent(parent);
			//Add it into pool list
			poolList.Add(newObject);
			//Set the new object active state
			newObject.SetActive(autoActive);
			//Return the new object to
			return newObject;
		}
    }
}
}