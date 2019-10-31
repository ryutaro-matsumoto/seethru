using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardSetButton : MonoBehaviour
{
	public bool isSelect = false;

	private Image image;
    // Start is called before the first frame update
    void Start()
    {
		image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelect){
			image.color = new Color(1f, 1f, 1f, 1f);
		}
		else {
			image.color = new Color(1f, 1f, 1f, 0.5f);
		}
    }

	public void Click(){
		GuardSetting gs = transform.parent.GetComponent<GuardSetting>();

		GameManager.soundManager.PlaySeInit((int)SoundManager.SEIndex.Corsor);


		if (isSelect){
			isSelect = false;
			gs.guardSetNum--;
		}
		else{
			if (gs.guardSetNum >= 2) {
				return;
			}
			isSelect = true;
			gs.guardSetNum++;
		}

	}
}
