using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class ToxicWater : MonoBehaviour
{
    public BoxCollider2D _PoolReturnArea;
    public int SpeedDivider = 4;

    private void FixedUpdate()
    {
        SlowMoveUpToCamera();
    }

    private void SlowMoveUpToCamera()
    {
        if (GameDataManager.Instance.PlayerManager.PlayerAttributes.Condition != PlayerCondition.Dead)
        {
            transform.position += Vector3.up * Time.deltaTime / SpeedDivider;
        }
    }

    private void Start()
    {
        StartCoroutine(CheckReturnToPoolArea());
    }

    private IEnumerator CheckReturnToPoolArea()
    {
        while (GameDataManager.Instance.PlayerManager.PlayerAttributes.Condition == PlayerCondition.Alive)
        {
            yield return new WaitForSeconds(3);
            foreach (var other in Physics2D.OverlapBoxAll((Vector2) transform.position + _PoolReturnArea.offset,
                _PoolReturnArea.size, 0f))
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.GetComponent<EnemyManager>().EnemyAttributes.MoveToPool();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameDataManager.Instance.PlayerManager.PlayerAttributes.WetnessCondition = WetnessCondition.Wet;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAttributes>().InitFreezing();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameDataManager.Instance.PlayerManager.PlayerAttributes.WetnessCondition = WetnessCondition.Dry;
        }
    }
}