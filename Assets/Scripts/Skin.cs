using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class Skin : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;
    private PhotonView _photonView;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _photonView = GetComponent<PhotonView>();

        var skinName = PlayerPrefs.GetString(PlayerPrefKeys.SKIN, "Mario");
        if (_photonView.IsMine)
        {
            _photonView.RPC("EquipSkin", RpcTarget.Others, skinName);
            EquipSkin(skinName);
        }
    }
    
    [PunRPC]
    private void EquipSkin(string id)
    {
        var path = $"{id}/{id}";
        var animator = Resources.Load<RuntimeAnimatorController>(path);
        _animator.runtimeAnimatorController = animator;
        var sprite = Resources.Load<Sprite>(path);
        _sprite.sprite = sprite;
    }
}