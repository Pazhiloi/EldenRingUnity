using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
  public class WorldSoundFXManager : MonoBehaviour
  {
    public static WorldSoundFXManager instance;

    [Header("Action Sounds")]
    public AudioClip rollSFX;

    private void Awake()
    {
      InitSingleton();
    }

    private void Start() {
      DontDestroyOnLoad(gameObject);
    }



    private void InitSingleton()
    {
      if (instance == null)
      {
        instance = this;
      }
      else
      {
        Destroy(gameObject);
      }
    }
  }
}
