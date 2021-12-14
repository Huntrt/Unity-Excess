using System.Collections.Generic; 
using UnityEngine;

public class Pool : MonoBehaviour
{
	//The pool contain all the object has create
	public List<GameObject> objectsPool = new List<GameObject>();
	//Turn this script into singleton
    public static Pool s; void Awake() {s = this;}

	//Create the object needed with wanted position, rotation, does it auto active upon create? and do it need to auto has parent?
    public GameObject Create(GameObject Need, Vector3 Position, Quaternion Rotation, bool autoActive = true, bool autoParent = true)
    {
		///If there is unactive object in pool
        if(objectsPool.Count > 0)
		{
			//Go through all the object in pool in reverse order
			for (int i = objectsPool.Count-1; i >= 0; i--)
			{
				//Remove any null object left from pool then skip it from being check
				if(objectsPool[i] == null) {objectsPool.RemoveAt(i); continue;}
				//Remove the "(Clone)" out of gameobject from pool name
				string objectName = objectsPool[i].name.Replace("(Clone)","");
				//If there is an unactive object in pool with the same name of object need to get
				if(!objectsPool[i].activeInHierarchy && string.Equals(Need.name, objectName))
				{
					//Set the polled object position
					objectsPool[i].transform.position = Position;
					//Set the polled object rotation
					objectsPool[i].transform.rotation = Rotation;
					//Set the pooled object parent if needed to
					Parenting(objectsPool[i], autoParent);
					//Active it depend if need to active
					objectsPool[i].SetActive(autoActive);
					//Send it to caller and no need to continue code
					return objectsPool[i];
				}
			}
		}
		///If there is no unactive object left in pool
		{
			//Create the needed object witth set position and rotation
			GameObject newObject = Instantiate(Need, Position, Rotation);
			//Set the new object parent if needed to
			Parenting(newObject, autoParent);
			//Add it into pool list
			objectsPool.Add(newObject);
			//Set the new object active state
			newObject.SetActive(autoActive);
			//Send new object to caller
			return newObject;
		}
    }

	void Parenting(GameObject child, bool parent) 
	{
		//Set the child object parent as this pooler object depend on if need to and none if not
		if(parent) {child.transform.SetParent(transform);} else {child.transform.SetParent(null);}
	}
}