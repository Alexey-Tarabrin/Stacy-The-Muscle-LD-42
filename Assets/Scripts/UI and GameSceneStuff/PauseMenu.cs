using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	public Button Resume;
	public Button Restart;

	private void OnEnable()
	{
		if (GameDataManager.Instance.PlayerManager.PlayerAttributes.Condition==PlayerCondition.Alive)
		{
			Resume.gameObject.SetActive(true);
			Restart.gameObject.SetActive(false);
		}
		else
		{
			Restart.gameObject.SetActive(true);
			Resume.gameObject.SetActive(false);
		}
	}
}
