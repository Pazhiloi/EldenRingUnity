using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MR
{
  public class PlayerInputManager : MonoBehaviour
  {
    public static PlayerInputManager instance;

    // THINK ABOUT GOALS IN STEPS
    // 2. MOVE CHARACTER BASED ON THOSE VALUES

    PlayerControls playerControls;
    [SerializeField] Vector2 movementInput;

    private void Awake()
    {
      InitSingleton();

    }

    private void Start()
    {
      DontDestroyOnLoad(gameObject);
      // WHEN THE SCENE CHANGES, RUN THIS LOGIC
      SceneManager.activeSceneChanged += OnSceneChange;
      instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
      // IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE OUR PLAYERS CONTROLS
      if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
      {
        instance.enabled = true;
      }
      // OTHERWISE WE MUST BE AT THE MAIN MENU, DISABLE OUR PLAYERS CONTROLS
      // THIS IS SO OUR PLAYER CANT MOVE AROUND IF WE ENTER THINGS LIKE A CHARACTER CREA
      else
      {
        instance.enabled = false;
      }
    }

    private void OnEnable()
    {
      playerControls.Enable();
    }

    private void OnDestroy()
    {
      // IF WE DESTROY THIS OBJECT, UNSUBSCRIBE FROM THIS EVENT
      SceneManager.activeSceneChanged -= OnSceneChange;
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