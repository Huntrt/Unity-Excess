using System.Collections.Generic; 
using UnityEngine;

public class Pool : MonoBehaviour
{
	//The pool contain all the object has create
	public List<GameObject> objectsPool = new List<GameObject>();
	//Turn this script into singleton
    public static Pool s; void Awake() {s = this;}

	//Create the object needed with wanted position, rotation and does it auto active upon create?
    public GameObject Create(GameObject Need, Vector3 Position, Quaternion Rotation, bool Active = true)
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
					//Set it position
					objectsPool[i].transform.position = Position;
					//Set it rotation
					objectsPool[i].transform.rotation = Rotation;
					//Active it depend if need to active
					objectsPool[i].SetActive(Active);
					//Send it to caller and no need to continue code
					return objectsPool[i];
				}
			}
		}
		///If there is no unactive object left in pool
		{
			//Create the needed object witth set position and rotation
			GameObject newObject = Instantiate(Need, Position, Rotation);
			//Set the new object parent as this object for organize
			newObject.transform.SetParent(transform);
			//Add it into pool list
			objectsPool.Add(newObject);
			//Set the new object active state
			newObject.SetActive(Active);
			//Send new object to caller
			return newObject;
		}
    }
}