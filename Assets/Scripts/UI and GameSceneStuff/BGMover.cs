using UnityEngine;

public class BGMover : MonoBehaviour
{
	 public Transform top;
	public Transform middle;
	public Transform bot;


	private void FixedUpdate()
	{
		if (Mathf.Abs(top.position.y - GameDataManager.Instance.MainCamera.transform.position.y) <= 1)
		{
			MoveUp();
		}
		if (Mathf.Abs(bot.position.y - GameDataManager.Instance.MainCamera.transform.position.y) <= 1)
		{
			MoveDown();
		}
	}

	private void MoveUp()
	{
		
		bot.position =top.position + Vector3.up*10.8f;
		var temp = top;
		top = bot;
		bot = middle;
		middle = temp;

	}

	private void MoveDown()
	{
		top.position =bot.position - Vector3.up*10.8f;
		var temp = top;
		top = middle;
		middle = bot;
		bot = temp;
	}
}
