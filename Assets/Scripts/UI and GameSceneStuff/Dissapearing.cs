using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissapearing : MonoBehaviour
{
	private Text _text;
	void Start ()
	{
		_text = GetComponent<Text>();
		StartCoroutine(DissapearSoon());
	}

	IEnumerator DissapearSoon()
	{
		yield return new WaitForSeconds(7);
		while (_text.color.a > 0)
		{
			var textColor = _text.color;
			textColor.a -= 0.01f;
			_text.color = textColor;
			yield return new WaitForSeconds(0.01f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
