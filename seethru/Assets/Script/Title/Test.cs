using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = 3.0f;
    Vector3 worldPos;

    // Start is called before the first frame update
    void Start()
    {
        worldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            transform.position = worldPos;
        }
    }
}
