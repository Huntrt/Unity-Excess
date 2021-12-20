using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
	[SerializeField] float speed;
	public Vector3 inputDirection;
	public Rigidbody2D Rigidbody;
	[SerializeField] Camera cameraFollow;
	[SerializeField] Vector2 mousePos;
	
    void Update()
    {
		//Running function
		MoveInput();
	}

	Vector2 velocity; void MoveInput()
	{
		//Set the input horizontal and vertical direction
		inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
        //Make diagonal movement no longer faster than vertical, horizontal
        velocity = inputDirection.normalized;
        //Add the speed to velocity
        velocity *= speed;
	}

	void FixedUpdate()
	{
		//Moving the player
		Rigidbody.MovePosition(Rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	void LateUpdate()
	{
		//If there is camera follow
		if(cameraFollow != null)
		{
			//Camera following player
			cameraFollow.transform.position = new Vector3(transform.position.x, transform.position.y,-10);
			//Get the mouse position if needed
			mousePos = cameraFollow.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}