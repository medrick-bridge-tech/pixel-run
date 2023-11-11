using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Properties")]
    public VariableJoystick joystick;
    public JumpButton jumpButton;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [Header("Sound Properties")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip kickSound;
    
    private float _distanceFromEarth = 0.5f;
    private float _xVector;
    private bool _moveable;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _moveable = false;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        InputHandler2D();
        HandleMove();
        HandleJump();
        HandleAnimation();
    }

    private void InputHandler2D()
    {
        if (joystick != null)
            _xVector = joystick.Horizontal;
        else
            _xVector = Input.GetAxisRaw("Horizontal");
    }

    private void HandleMove()
    {
        if (_animator.GetBool("IsAlive") && _moveable)
            Run();

        if (Mathf.Round(_xVector) != 0)
        {
            ChangeScale(new Vector2(Mathf.Round(_xVector),transform.localScale.y));
        }
    }

    private void HandleJump()
    {
        if(jumpButton.isJumpButtonTouched)
            if (IsOnGround() && _moveable)
            {
                _audioSource.PlayOneShot(jumpSound);
                Jump();
            }
    }

    private void Run()
    {
        transform.position += (Vector3)new Vector2(_xVector * moveSpeed * Time.deltaTime, 0);
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _rigidbody2D.AddForce(transform.up*jumpSpeed*2,ForceMode2D.Impulse);
            _audioSource.PlayOneShot(kickSound);
        }
    }

    private bool IsOnGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -transform.up, _distanceFromEarth, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
            return true;

        return false;
    }

    public void Unfreeze()
    {
        _moveable = true;
    }
}
