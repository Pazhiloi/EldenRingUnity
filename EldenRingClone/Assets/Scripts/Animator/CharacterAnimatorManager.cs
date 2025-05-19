using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace MR
{
  public class CharacterAnimatorManager : MonoBehaviour
  {
    CharacterManager character;
    float vertical, horizontal;

    protected virtual void Awake()
    {
      character = GetComponent<CharacterManager>();
    }

    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
    {
      character.animator.SetFloat("Horizontal", horizontalMovement, 0.1f, Time.deltaTime);
      character.animator.SetFloat("Vertical", verticalMovement, 0.1f, Time.deltaTime);
    }

    public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = true, bool canMove = true)
    {
      character.applyRootMotion = applyRootMotion;
      character.animator.CrossFade(targetAnimation, 0.2f);
      character.isPerformingAction = isPerformingAction;
      character.canRotate = canRotate;
      character.canMove = canMove;

      character.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);

    }
  }
}
