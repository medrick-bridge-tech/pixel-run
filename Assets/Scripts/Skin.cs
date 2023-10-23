using UnityEditor;
using UnityEngine;

public class Skin : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.HasKey("skin"))
        {
            var skinPrefab = Resources.Load<GameObject>(PlayerPrefs.GetString("skin"));
            _animator.runtimeAnimatorController = skinPrefab.GetComponent<Animator>().runtimeAnimatorController;
            _sprite.sprite = skinPrefab.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            _animator.runtimeAnimatorController = Resources.Load<GameObject>("Mario_Prefab").GetComponent<Animator>().runtimeAnimatorController;
        }
    }
}