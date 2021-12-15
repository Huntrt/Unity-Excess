using UnityEngine;

public class LogTesting : MonoBehaviour
{
	//Print random test log
    public void PrintTest() {ExcessPackage.LogEx.Debug.Log("test - " + Random.Range(0,100));}
}