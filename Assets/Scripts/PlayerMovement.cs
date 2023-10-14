using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Timer timer;

    private float _distanceFromEarth = 0.5f;
    private float _xVector;
    private bool _moveable;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _moveable = false;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartCoroutine(CheckTimer());
    }

    private void Update()
    {
        InputHandler2D();
        HandleMove();
        HandleAnimation();
    }

    private void InputHandler2D()
    {
        _xVector = Input.GetAxisRaw("Horizontal");
    }
    
    private void HandleMove()
    {
        if (_animator.GetBool("IsAlive") && _moveable)
        {
            Run();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround() && _moveable)
        {
            Jump();
        }

        if (_xVector != 0)
        {
            ChangeScale(new Vector2(_xVector,transform.localScale.y));
        }
    }

    private void Run()
    {
        transform.position += (Vector3)new Vector2(_xVector * moveSpeed * Time.deltaTime, 0);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private void ChangeScale(Vector2 value)
    {
        transform.localScale = value;
    }

    private void HandleAnimation()
    {
        if (_xVector != 0 && !_animator.GetBool("IsJump"))
            _animator.SetFloat("Speed", 1);
        else
            _animator.SetFloat("Speed", 0);

        if (!IsOnGround())
            _animator.SetBool("IsJump", true);
        else
            _animator.SetBool("IsJump", false);
    }

    private bool IsOnGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -transform.up, _distanceFromEarth, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
            return true;
        
        return false;
    }

    private IEnumerator CheckTimer()
    {
        while (true)
        {
            if (timer.ReturnTime() <= 0)
            {
                Debug.Log("mOVEANLE");
                _moveable = true;
                yield break;
            }
            Debug.Log(timer.ReturnTime());
            yield return null;
        }
    }
}
