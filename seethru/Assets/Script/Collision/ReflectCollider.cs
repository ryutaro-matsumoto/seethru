using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectCollider : MonoBehaviour{
	[HideInInspector]
	public Vector2 rayVector;
	public bool isDebug = false;

	BoxCollider2D reflectCollision;


    // Start is called before the first frame update
    void Start(){
		float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
		rayVector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
		reflectCollision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update(){
		float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
		rayVector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
	}

	public Vector2 ReflectVector(Vector2 vec){
		Vector2 ans = Vector2.zero;
		ans = vec - 2 * Vector2.Dot(vec, rayVector) * rayVector;
		ans.Normalize();
		return ans;

	}
}
