using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	// コンポーネント
	Rigidbody2D rb2d;
	Pool bulletPool;
	Player player;
    MrsClient mrsCli;

	// ゲームオブジェクト
	GameObject pointer;
	GameObject mainCamera;

	// インスペクター調整値
	public float moveSpeed;
	public float moveAngle;
	public float rotateSpeed;
	public float moveForceMultiplier;

	[SerializeField]
	private GameObject bulletStart;

	// プライベート
	float inputMove;
    bool inputAttack;
	bool inputAttackBuff;

	Vector2 playerVector;

    public bool InputAttack { get { return inputAttack; } }

	// Start is called before the first frame update
	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		pointer = GameObject.Find("Pointer");

		bulletPool = GameObject.Find("BulletPool").GetComponent<Pool>();

		player = GetComponent<Player>();

		playerVector = new Vector2(0.0f, 1.0f);

        mrsCli = GameObject.Find("ClientObject").GetComponent<MrsClient>();
    }

    // Update is called once per frame
    void Update() {
		GetInput();
    }

	private void FixedUpdate() {
		AngleCalc();

		Move();

		Attack();
	}

	/// <summary>
	/// ポインタとプレイヤーの角度
	/// </summary>
	private void AngleCalc(){
		Vector2 pointerPosition = pointer.transform.position;
		Vector2 pointerVector = pointerPosition - (Vector2)transform.position;
		float cross = Vector2Cross(playerVector, pointerVector);


		float rad = Mathf.Asin(cross / playerVector.magnitude/ pointerVector.magnitude);
		float angle = rad * Mathf.Rad2Deg;

		Quaternion qt = transform.rotation;

		if(cross < 0){
			qt.eulerAngles -= new Vector3(0f, 0f, rotateSpeed);
			if(qt.eulerAngles.z < angle + transform.eulerAngles.z){
				qt.eulerAngles = new Vector3(0f, 0f, angle + transform.eulerAngles.z);
			}
		}
		else {
			qt.eulerAngles += new Vector3(0f, 0f, rotateSpeed);
			if (qt.eulerAngles.z > angle + transform.eulerAngles.z) {
				qt.eulerAngles = new Vector3(0f, 0f, angle + transform.eulerAngles.z);
			}
		}

		transform.rotation = qt;

		qt.eulerAngles = new Vector3(0f, 0f, qt.eulerAngles.z + 90f);

		playerVector.x = moveSpeed * Mathf.Cos(qt.eulerAngles.z * Mathf.Deg2Rad);
		playerVector.y = moveSpeed * Mathf.Sin(qt.eulerAngles.z * Mathf.Deg2Rad);

		player.angle = qt.eulerAngles.z;
	}


	/// <summary>
	/// 入力取得
	/// </summary>
	private void GetInput(){
		inputMove = Input.GetAxis("Fire1");

		if(0 != Input.GetAxis("Fire2")){
			inputAttack = true;
		}
		else{
			inputAttack = false;
		}
	}


	/// <summary>
	/// 移動処理
	/// </summary>
	private void Move(){
		Vector2 moveVector = playerVector * inputMove;
		rb2d.AddForce(moveForceMultiplier * (moveVector - rb2d.velocity));
	}


	private void Attack(){
		if(inputAttack && !inputAttackBuff){
			bulletPool.Place(bulletStart.transform.position, transform.rotation);
            mrsCli.SendShootData(bulletStart.transform.position.x, bulletStart.transform.position.y, transform.rotation.z);
		}
		inputAttackBuff = inputAttack;
	}


	public float Vector2Cross(Vector2 vec1, Vector2 vec2){
		return vec1.x * vec2.y - vec2.x * vec1.y;
	}
}
