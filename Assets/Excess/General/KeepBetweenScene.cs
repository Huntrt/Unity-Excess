using UnityEngine;

[DefaultExecutionOrder(-5)] //Set execution order in script
public class KeepBetweenScene : MonoBehaviour
{
	/// The component rename the component if wanted more version
	/// Could just copy and paste this code to integrate into any code 
	static KeepBetweenScene keeped;

	void Awake()
	{
		//Move this component to don't destroy on load if this is the first one
		//? Only move 1 instance of this object to don't destroy on load
		if(keeped == null) {keeped = this; DontDestroyOnLoad(this);}
		//Destroy gameobject if the object contain this component are not in don't destroy on load
		//? Prevent duplicate of gameobject has this component.
		if(gameObject.scene.name != "DontDestroyOnLoad") {Destroy(gameObject);}
	}
}