using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
  public class PlayerCamera : MonoBehaviour
  {
    public static PlayerCamera instance;
    public Camera cameraObject;

    private void Awake()
    {
      InitSingleton();
    }

    private void Start()
    {
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
