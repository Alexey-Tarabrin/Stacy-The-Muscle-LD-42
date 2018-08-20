using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public enum EnemyCondition
{
    Alive,
    Dead,
    Freesing,
    Frozen
}

public class EnemyAttributes : GenericManageableClass<EnemyManager>
{
    [SerializeField] private int _maxHealth = 1;
    private int _currHealth;
    private bool _isStayOnSmth;
    public EnemyCondition Condition { get; private set; }
    private Rigidbody2D _rigitBody2D;
    private SpriteRenderer[] _spriteRenderers;

    private void Awake()
    {
        _spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        _rigitBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        ChangeCondition(EnemyCondition.Alive);
    }

    public void GotDamage(int Damage)
    {
        if (Condition == EnemyCondition.Alive)
        {
            _manager.EnemyAudioManager.OnHitAudioSource.PlayRandom();
            _currHealth -= Damage;
            if (_currHealth <= 0)
            {
                GameDataManager.Instance.ReadyToFreezeEnemy.Add(this);
                ChangeCondition(EnemyCondition.Dead);
            }
        }
    }

    private void ChangeCondition(EnemyCondition enemyCondition)
    {
        Condition = enemyCondition;
        switch (Condition)
        {
            case EnemyCondition.Alive:
                _isStayOnSmth = false;
                _manager.EnemyMovement.Reset();
                _currHealth = _maxHealth;
                ChangeColorOfSprites(Color.white);
                _rigitBody2D.bodyType = RigidbodyType2D.Dynamic;
                break;
            case EnemyCondition.Dead:
                ChangeColorOfSprites(Color.red);

                break;
            case EnemyCondition.Freesing:
                break;
            case EnemyCondition.Frozen:
                ChangeColorOfSprites(Color.blue);
                _rigitBody2D.bodyType = RigidbodyType2D.Static;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ChangeColorOfSprites(Color spriteRendererColor)
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = spriteRendererColor;
        }
    }

    public void MoveToPool()
    {
        GameDataManager.Instance.EnemyGenerator.DisabledEnemies.Add(_manager);
        gameObject.SetActive(false);
    }

    public void InitFreezing()
    {
        ChangeCondition(EnemyCondition.Freesing);
        StartCoroutine(ReadyToFreeze());
    }

    private IEnumerator ReadyToFreeze()
    {
        yield return new WaitUntil(() => _isStayOnSmth);
        Freeze();
    }

    private void Freeze()
    {
        ChangeCondition(EnemyCondition.Frozen);
        GameDataManager.Instance.ReadyToFreezeEnemy.Remove(this);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (Condition == EnemyCondition.Freesing)
        {
            if (other.gameObject.CompareTag("Enemy") &&
                other.gameObject.GetComponent<EnemyAttributes>().Condition == EnemyCondition.Frozen ||
                other.gameObject.CompareTag("Connectable"))
            {
                _isStayOnSmth = true;
            }
        }
    }
}