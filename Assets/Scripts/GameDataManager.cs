using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : GenericSingletonClass<GameDataManager>
{
    public PlayerManager PlayerManager { get; private set; }
    public EnemyGenerator EnemyGenerator { get; private set; }
    public Camera MainCamera { get; private set; }
    public List<EnemyAttributes> ReadyToFreezeEnemy;
    public GUIManager GUIManager { get; private set; }
    public bool IsPaused;

    public override void Awake()
    {
        ReadyToFreezeEnemy = new List<EnemyAttributes>();
        if (EnemyGenerator == null)
            EnemyGenerator = FindObjectOfType<EnemyGenerator>();

        if (PlayerManager == null)
            PlayerManager = FindObjectOfType<PlayerManager>();
        
        if (GUIManager == null)
            GUIManager = FindObjectOfType<GUIManager>();
        
        if (MainCamera == null)
            MainCamera = FindObjectOfType<Camera>();
    }

    //FindWithTag
}