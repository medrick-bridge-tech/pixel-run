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
        
        var skin = PlayerPrefs.GetString(PlayerPrefKeys.SKIN, "Mario");
        var path = $"{skin}/{skin}";
        var animator = Resources.Load<RuntimeAnimatorController>(path);
        _animator.runtimeAnimatorController = animator;
        var sprite = Resources.Load<Sprite>(path);
        _sprite.sprite = sprite;
    }
}