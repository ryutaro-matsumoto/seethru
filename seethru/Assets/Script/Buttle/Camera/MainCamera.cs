using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	[SerializeField]
	private LookatObject lookatObject;

	private Camera c_camera;

	public float scalingSpeed = 0.05f;

	public float size = 2f;

	public float minSize = 4f;

	public float adjustPos = 2f;


	private Vector3 oldPostion = Vector3.zero;
	private void Start() {
		c_camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void LateUpdate(){
		Vector3 newPosition = lookatObject.transform.position;

		transform.LookAt(lookatObject.transform);

		newPosition.y -= adjustPos;


		if (lookatObject.Distance.y * ((float)Screen.width / (float)Screen.height) < lookatObject.Distance.x){
			newPosition.z = Mathf.Lerp(transform.position.z, -(((lookatObject.Distance.x * ((float)Screen.height / (float)Screen.width)) / 2f) / Mathf.Tan(30f * Mathf.Deg2Rad) + size), scalingSpeed);
			c_camera.orthographicSize = Mathf.Lerp(c_camera.orthographicSize, ((lookatObject.Distance.x * ((float)Screen.height / (float)Screen.width)) / 2f) + size, scalingSpeed);
		}
		else{
			newPosition.z = Mathf.Lerp(transform.position.z, -((lookatObject.Distance.y / 2) / Mathf.Tan(30f * Mathf.Deg2Rad) + size), scalingSpeed);
			c_camera.orthographicSize = Mathf.Lerp(c_camera.orthographicSize, (lookatObject.Distance.y / 2f) + size, scalingSpeed);
		}

		if(c_camera.orthographicSize < minSize){
			c_camera.orthographicSize = minSize;
		}

		transform.position = newPosition;

	}
}
