using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private bool _isAbleToFlex = true;

    private void Update()
    {
        if (!(GameDataManager.Instance.IsPaused || EventSystem.current.IsPointerOverGameObject()))
        {
            if (Input.GetMouseButtonDown(1) && _isAbleToFlex)
            {
                StartCoroutine(DisableFlex());
                GameDataManager.Instance.PlayerManager.PlayerAttributes.Flex();
            }
            if (Input.GetMouseButtonDown(0))
            {
                GameDataManager.Instance.PlayerManager.PlayerMovement.HitMove();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    IEnumerator DisableFlex()
    {
        _isAbleToFlex = false;
        yield return new WaitForSeconds(3);
        _isAbleToFlex = true;
    }
}