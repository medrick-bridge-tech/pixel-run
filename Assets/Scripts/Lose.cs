using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Lose : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private float loseJump;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private CinemachineVirtualCamera _cvm;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _cvm = GetComponentInChildren<CinemachineVirtualCamera>();
        _animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("do");
            _animator.SetBool("IsAlive",false);
            Destroy(GetComponent<BoxCollider2D>());
            _rigidbody2D.AddForce(new Vector2(0,loseJump),ForceMode2D.Impulse);
            GameObject crow = new GameObject();
            crow.transform.position = transform.position;
            _cvm.Follow = crow.transform;
            GetComponent<AudioSource>().PlayOneShot(loseSound);
            StartCoroutine(DisplayLoseUI());
        }
    }

    private IEnumerator DisplayLoseUI()
    {
        yield return new WaitForSeconds(3f);
        loseUI.SetActive(true);
    }
}