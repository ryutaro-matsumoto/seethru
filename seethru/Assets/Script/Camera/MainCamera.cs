using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	[SerializeField]
	private LookatObject lookatObject;

	public float t;


    // Update is called once per frame
    void LateUpdate()
    {
		Vector3 newPosition = transform.position;
		newPosition = Vector3.Lerp(transform.position, lookatObject.transform.position, t);

		newPosition.z = transform.position.z;

		transform.position = newPosition;


    }
}
