using System;
using UnityEngine;
using UnityEngineInternal;

public class PlayerMovement : GenericManageableClass<PlayerManager>
{
    public CapsuleCollider2D LegsCollider2D;
    public HingeJoint2D HingeJoint2D;
    [SerializeField] private float _groundedRadius = 0.1f;
    [SerializeField] [Range(1000, 3000)] private float _jumpForce = 1500;
    [SerializeField] [Range(50, 300)] private int _moveForce = 100;

    private bool _floored = true;
    private Rigidbody2D _rigidbody2D;
    private int _currDir = 1;
    private float _horizontalInput;
    private bool _jumpInput;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _jumpInput = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        if (GameDataManager.Instance.PlayerManager.PlayerAttributes.Condition == PlayerCondition.Dead)
            return;
        CheckFloor();
        VerticalMove();
        HorizontalMove();
    }

    public void HitMove()
    {
        _manager.PlayerAudioManager.AudioSwingSource.PlayRandom();
        JointMotor2D newMotor = HingeJoint2D.motor;
        newMotor.motorSpeed *= -1;
        HingeJoint2D.motor = newMotor;
    }

    private void VerticalMove()
    {
        if (!_jumpInput || !_floored) return;
        _floored = false;
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }

    private void HorizontalMove()
    {
        if (Mathf.Abs(_horizontalInput) > Vector2.kEpsilon)
        {
            if (Mathf.Sign(_horizontalInput) > 0)
            {
                if (_rigidbody2D.velocity.x < 10)
                    _rigidbody2D.AddForce(_moveForce * Vector2.right);
            }
            else
            {
                if (_rigidbody2D.velocity.x > -10)
                    _rigidbody2D.AddForce(_moveForce * Vector2.left);
            }

            if (Math.Abs(_currDir - Mathf.Sign(_horizontalInput)) > Vector2.kEpsilon)
            {
                _currDir *= -1;
                JointMotor2D newMotor = HingeJoint2D.motor;
                newMotor.motorSpeed *= -1;
                HingeJoint2D.motor = newMotor;

                JointAngleLimits2D newJointAngleLimits2D = HingeJoint2D.limits;
                newJointAngleLimits2D.max *= -1;
                newJointAngleLimits2D.min *= -1;
                HingeJoint2D.limits = newJointAngleLimits2D;

                var prevRotation = HingeJoint2D.transform.rotation;
                transform.localScale = new Vector3(_currDir, 1, 1);
                HingeJoint2D.transform.rotation = prevRotation;
            }
        }
    }

    private void CheckFloor()
    {
        if (_floored) return;

        foreach (var other in Physics2D.OverlapCapsuleAll((Vector2) transform.position + LegsCollider2D.offset,
            LegsCollider2D.size + Vector2.one * _groundedRadius, LegsCollider2D.direction, 0f))
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                _floored = false;
            }

            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Connectable"))
            {
                _floored = true;
            }
        }
    }
}