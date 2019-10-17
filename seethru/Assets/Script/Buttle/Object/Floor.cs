using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	//初期の大きさ
	public int maxWidth = 16;
	public int maxHeight = 10;

	public Vector2 size;

	// Component
	private SpriteRenderer sr;
	private BoxCollider2D bc2d;

    void Start(){
		size.x = maxWidth;
		size.y = maxHeight;

		sr = GetComponent<SpriteRenderer>();
		bc2d = GetComponent<BoxCollider2D>();

		sr.size = size;
		bc2d.size = size;
	}

	void Update(){
        
    }


	public void FallFloor(){
		size.x -= 2;
		size.y -= 2;

		sr.size = size;
		bc2d.size = size;
	}

}
