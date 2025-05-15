using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MR
{
  public class WorldSaveGameManager : MonoBehaviour
  {
    public static WorldSaveGameManager instance;
    [SerializeField] private int worldSceneIndex = 1;
    private void Awake()
    {
      InitSingleton();
    }

    private void Start()
    {
      DontDestroyOnLoad(gameObject);
    }


    public IEnumerator LoadNewGame()
    {
      AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
      yield return null;
    }

    public int GetWorldSceneIndex()
    {
      return worldSceneIndex;
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