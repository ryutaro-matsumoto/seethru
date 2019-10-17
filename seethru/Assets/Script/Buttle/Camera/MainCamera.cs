using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	[SerializeField]
	private LookatObject lookatObject;

	private Camera c_camera;

	public float moveSpeed = 0.1f;
	public float scalingSpeed = 0.1f;

	public float size = 2f;

	public float minSize = 4f;

	private void Start() {
		c_camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void LateUpdate(){
		Vector3 newPosition = transform.position;
		newPosition = Vector3.Lerp(transform.position, lookatObject.transform.position, moveSpeed);

		newPosition.z = transform.position.z;

		transform.position = newPosition;

		Debug.Log(lookatObject.Distance);

		if (lookatObject.Distance.y * ((float)Screen.width / (float)Screen.height) < lookatObject.Distance.x){
			c_camera.orthographicSize = Mathf.Lerp(c_camera.orthographicSize, ((lookatObject.Distance.x * ((float)Screen.height / (float)Screen.width)) / 2f) + size, scalingSpeed);
		}
		else{
			c_camera.orthographicSize = Mathf.Lerp(c_camera.orthographicSize, (lookatObject.Distance.y / 2f) + size, scalingSpeed);
		}

		if(c_camera.orthographicSize < minSize){
			c_camera.orthographicSize = minSize;
		}
	}
}
