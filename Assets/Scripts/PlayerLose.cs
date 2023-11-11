using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerLose : MonoBehaviour
{
    //[SerializeField] private GameObject loseUI;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private float loseJump;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private PhotonView _photonView;

    public Action<PhotonView> onLoseGame;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            if (_photonView.IsMine)
            {
                _animator.SetBool("IsAlive", false);
                Destroy(GetComponent<BoxCollider2D>());
                _rigidbody2D.AddForce(new Vector2(0, loseJump), ForceMode2D.Impulse);
                GetComponent<AudioSource>().PlayOneShot(loseSound);
                onLoseGame.Invoke(_photonView);
                StartCoroutine(Die());
            }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.Destroy(gameObject);
    }
}