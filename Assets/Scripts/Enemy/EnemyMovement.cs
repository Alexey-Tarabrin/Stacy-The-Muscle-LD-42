using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : GenericManageableClass<EnemyManager> {

	[SerializeField] [Range(300, 1000)] private float _forcePower = 200;
	private bool _isTouchedFloor;
	private Rigidbody2D _rigitBody2D;

	private void Awake()
	{
		_rigitBody2D = GetComponent<Rigidbody2D>();
	}

	public void Reset()
	{
		_isTouchedFloor = false;
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (!_isTouchedFloor && !other.gameObject.CompareTag("Wall"))
		{
			_isTouchedFloor = true;
			StartMoving();
		}
	}

	private void StartMoving()
	{
		StartCoroutine(PushToPlayer());
	}
	private IEnumerator PushToPlayer()
	{
		while (_manager.EnemyAttributes.Condition == EnemyCondition.Alive)
		{
			_rigitBody2D.AddForce(
				((GameDataManager.Instance.PlayerManager.transform.position - transform.position).normalized +
				 Vector3.up) *_forcePower);
			yield return new WaitForSeconds(2);
		}
	}
}
