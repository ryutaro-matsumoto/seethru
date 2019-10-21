using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultCheck : MonoBehaviour
{
	public string resultScene;

	private bool onResult = false;
    void Update()
    {
		if(GameManager.livePlayer <= 1 && !onResult){
			SceneManager.LoadScene(resultScene, LoadSceneMode.Additive);
			onResult = true;
		}   
    }
}
