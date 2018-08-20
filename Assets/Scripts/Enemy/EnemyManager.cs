using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : GenericManageableClass<EnemyGenerator> {

	public EnemyAttributes EnemyAttributes { get; private set; }
	public EnemyAudioManager EnemyAudioManager { get; private set; }
	public EnemyMovement EnemyMovement { get; private set; }

	private void Awake()
	{
		EnemyAttributes = GetComponent<EnemyAttributes>();
		EnemyAttributes.SetManager(this);

		EnemyAudioManager = GetComponentInChildren<EnemyAudioManager>();
		EnemyAudioManager.SetManager(this);
		
		EnemyMovement = GetComponent<EnemyMovement>();
		EnemyMovement.SetManager(this);
	}
}
