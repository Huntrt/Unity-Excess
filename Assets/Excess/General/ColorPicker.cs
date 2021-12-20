using System.Collections; 
using UnityEngine.Events;
using UnityEngine; 

public class ColorPicker : MonoBehaviour
{
	//The event that send the color that got pick
	public class EventPick : UnityEvent<Color> {} public EventPick pickColor = new EventPick();
	//The color mouse currently hover over
	public Color hoverColor;
	//Does currently picking any color
	public bool arePicking = false;
	//The gamescreen convert to Texture2D
	[SerializeField] Texture2D screenTexture;

	//Begin capture the screen with left mouse to pick color
	public void BeginPickColor() {StartCoroutine(CaptureScreen(KeyCode.Mouse0));}
	//Begin capture the screen and the key that will handle pick color
	public void BeginPickColor(KeyCode pickKey) {StartCoroutine(CaptureScreen(pickKey));}

	///Basically it take screenshot of the game then apply the screenshot into an Texture2D
	IEnumerator CaptureScreen(KeyCode key)
	{
		//Set the size of the screen texture by using the screen width and height
		screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		//Wait for an frame to get texture size
		yield return new WaitForEndOfFrame();
		//Get all the pixel info currently on the screen
		screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
		//Apply those pixel into the screen texture
		screenTexture.Apply();
		//Begin pick color with key
		arePicking = true; StartCoroutine(Picking(key));
	}
	
	IEnumerator Picking(KeyCode key)
	{
		//If currently picking color
		while (arePicking)
		{
			//Get the color of the pixel under cursor position base on the captured texture
			hoverColor = screenTexture.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);
			//Upon press key send the picked color event with the color currently hover then no longer pick color
			if(Input.GetKeyUp(key)) {pickColor.Invoke(hoverColor); arePicking = false;}
			yield return null;
		}
	}
}