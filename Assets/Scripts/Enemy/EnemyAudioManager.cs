using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : GenericManageableClass<EnemyManager>
{
	public CustomAudioSource OnHitAudioSource { get;private set; }

	private void Awake()
	{
		OnHitAudioSource=GetComponentInChildren<CustomAudioSource>();
	}
}
