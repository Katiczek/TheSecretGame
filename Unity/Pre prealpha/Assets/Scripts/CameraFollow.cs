using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public bool CameraAutofollow = false;
	public Transform target;
	public float smoothing = 5f;
	public float edgeSensitivity = 5f;
	public float angle = 30f;
	public float scrollSensitivity = 5f;
	public Transform Camera;

	Vector3 offset;

	void Awake()
	{
		transform.position = target.position + new Vector3 (0f, 2f, -5f);
		Camera.LookAt (target);
	}

	void Start()
	{
		offset = transform.position - target.transform.position;
	}

	void FixedUpdate()
	{
		//transform.LookAt (target); // ewentualnie kiedyś do zrobienia w autofollow. 
		if (CameraAutofollow == true) {
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		} 
		else {
			Vector3 MousePos = Input.mousePosition;
			Vector2 MouseScroll = Input.mouseScrollDelta;





			if (MousePos.x <= 5 || Input.GetKey ("left")) 	
				transform.Translate (Vector3.left * Time.deltaTime * edgeSensitivity);
			if (MousePos.x >= Screen.width - 5 || Input.GetKey ("right")) 		
				transform.Translate (Vector3.right * Time.deltaTime * edgeSensitivity);

			if (MousePos.y <= 5 || Input.GetKey ("down")) 		
				transform.Translate (Vector3.back * Time.deltaTime * edgeSensitivity);
			if (MousePos.y >= Screen.height - 5 || Input.GetKey ("up")) 		
				transform.Translate (Vector3.forward * Time.deltaTime * edgeSensitivity);

			if (MouseScroll.y != 0) 		
				transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(0f, MouseScroll.y) * scrollSensitivity, Time.deltaTime * smoothing);
		}
			


	}
}
