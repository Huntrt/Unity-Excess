using System.Collections.Generic; using UnityEngine;

//? Just F2 the component name if wanted to rename to "Pool" or anything
public class BasicPool : MonoBehaviour
{
	//The pool contain all the object
	public List<GameObject> objectsPool;

	//Turn this script into singleton
    public static BasicPool get; void Awake() {get = this;}

	//Create an clean new list for object pool
	void Start() {objectsPool = new List<GameObject>();}

	//Sending the wanted object to whoever call this function
    public GameObject Object (GameObject Need)
    {
		//If there is object in pool
        if(objectsPool.Count > 0)
		{
			//Go through all the object in pool in reverse order
			for (int i = objectsPool.Count-1; i >= 0; i--)
			{
				//Remove an null object from bool
				if(objectsPool[i] == null) {objectsPool.RemoveAt(i);}
				//If there is an unactive object in pool with the same name of object need to get
				else if(!objectsPool[i].activeInHierarchy && Need.name+"(Clone)" == objectsPool[i].name)
				{
					//Send it 
					return objectsPool[i];
				}
			}
		}
		//If there is no unactive object in pool
		{
			//Create the needed object
			GameObject newObject = Instantiate(Need);
			//Add it into pool list
			objectsPool.Add(newObject);
			//Deactive it
			newObject.SetActive(false);
			//Send it
			return newObject;
		}
    }
}