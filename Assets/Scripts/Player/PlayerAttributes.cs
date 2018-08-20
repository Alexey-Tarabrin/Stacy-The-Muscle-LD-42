using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.UIElements.StyleEnums;
using UnityEngine.UI;

public enum WetnessCondition
{
    Dry,
    Wet
}

public enum PlayerCondition
{
    Alive,
    Dead
}

public class PlayerAttributes : GenericManageableClass<PlayerManager>
{
    [SerializeField] private int _maxHealth;
    private int _health;
    public WetnessCondition WetnessCondition;
    public PlayerCondition Condition { get; private set; }

    private void Awake()
    {
        WetnessCondition = WetnessCondition.Dry;
        Condition = PlayerCondition.Alive;
    }

    private void Start()
    {
        StartCoroutine(HealthManagment());
    }

    IEnumerator HealthManagment()
    {
        GameDataManager.Instance.GUIManager.HealthBar.maxValue = _maxHealth;
        _health = _maxHealth;
        int wetness = 0;
        while (Condition == PlayerCondition.Alive)
        {
            switch (WetnessCondition)
            {
                case WetnessCondition.Dry:
                    _health = Mathf.Min(_maxHealth, _health + 10);
                    wetness = 0;
                    break;
                case WetnessCondition.Wet:
                    wetness++;
                    if (wetness > 3)
                    {
                        _manager.PlayerAudioManager.AudioWasHitSource.PlayRandom();
                        _health -= 25;
                        GameDataManager.Instance.GUIManager.HealthBar.value = _health;
                        if (_health <= 0)
                        {
                            _manager.PlayerAnimationManager.AnimationToDeath(true);
                            Condition = PlayerCondition.Dead;
                            GameDataManager.Instance.GUIManager.ActivatePause();
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GameDataManager.Instance.GUIManager.HealthBar.value = _health;
            yield return new WaitForSeconds(1);
        }
    }

    public void Flex()
    {
        if (Condition == PlayerCondition.Alive)
        {
            _manager.PlayerAudioManager.AudioFlexSource.PlayRandom();
            _manager.PlayerAnimationManager.AnimationToFlex();
            foreach (var enemy in GameDataManager.Instance.ReadyToFreezeEnemy)
            {
                enemy.InitFreezing();
            }
        }
    }
}