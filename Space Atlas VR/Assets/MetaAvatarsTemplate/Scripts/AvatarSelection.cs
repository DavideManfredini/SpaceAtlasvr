using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chiligames.MetaAvatarsPun
{
    
    public class AvatarSelection : MonoBehaviour
    {
        SampleEntity avatarEntity;
        PlayerManager playerManager;

        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            avatarEntity = GetComponent<SampleEntity>();    
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerManager.ChangeMyAvatar(avatarEntity.GetAssetPath());
            }
        }
    }
}
