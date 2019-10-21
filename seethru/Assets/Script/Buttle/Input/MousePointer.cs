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
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		int layerMask = (1 << LayerMask.NameToLayer("MouseScreen")); //適当なレイヤーマスクを設定するよ

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
			//レイが当たった位置を得るよ
			Vector3 pos = hit.point;
			transform.position = pos;
		}

		//position = Input.mousePosition;
		//position.z = 10f;


		//screenToWorldPoint = Camera.main.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		//screenToWorldPoint.z = 0f;

		//gameObject.transform.position = screenToWorldPoint;
    }
}
