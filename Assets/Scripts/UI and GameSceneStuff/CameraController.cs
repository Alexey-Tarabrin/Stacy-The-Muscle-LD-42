using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private void FixedUpdate()
    {
        MoveToPosition(GameDataManager.Instance.PlayerManager.transform.position+Vector3.up);
    }

    private void MoveToPosition(Vector3 taretPos)
    {
        transform.position += (taretPos - transform.position).y * Vector3.up * Time.fixedDeltaTime * 3;
        GameDataManager.Instance.GUIManager.HeightText.text = "Height: " + (int) (transform.position.y + 1.5 - 0.35);
    }
}