using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
	public int stageMax = 1;

	public int selectStage = 1;

	private void Start() {
		stageMax = transform.GetChild(0).childCount;
		GameManager.stageSelect = this;
	}



	public void StageNumAdd(){
		transform.GetChild(0).GetChild(selectStage - 1).gameObject.SetActive(false);
		selectStage++;
		if(selectStage > stageMax){
			selectStage = 1;
		}
		transform.GetChild(0).GetChild(selectStage - 1).gameObject.SetActive(true);
        GameManager.connection.SendStageNumber(selectStage);
    }

	public void StageNumSub() {
		transform.GetChild(0).GetChild(selectStage - 1).gameObject.SetActive(false);
		selectStage--;
		if (selectStage < 1) {
			selectStage = stageMax;
		}
		transform.GetChild(0).GetChild(selectStage - 1).gameObject.SetActive(true);
        GameManager.connection.SendStageNumber(selectStage);
	}
}
