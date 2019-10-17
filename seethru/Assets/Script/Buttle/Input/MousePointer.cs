using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
	private Vector3 position;

	private Vector3 screenToWorldPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		position = Input.mousePosition;

		position.z = 10f;

		screenToWorldPoint = Camera.main.ScreenToWorldPoint(position);

		gameObject.transform.position = screenToWorldPoint;
    }
}
