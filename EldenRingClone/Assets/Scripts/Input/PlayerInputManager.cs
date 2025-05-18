using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MR
{
  public class PlayerInputManager : MonoBehaviour
  {
    public static PlayerInputManager instance;
    public PlayerManager player;

    PlayerControls playerControls;
    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;
    [Header("CAMERA MOVEMENT INPUT")]
    [SerializeField] Vector2 cameraInput;

    public float cameraVerticalInput;
    public float cameraHorizontalInput;

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
      if (playerControls == null)
      {
        playerControls = new PlayerControls();
        playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
      }

      playerControls.Enable();
    }

    private void OnDestroy()
    {
      // IF WE DESTROY THIS OBJECT, UNSUBSCRIBE FROM THIS EVENT
      SceneManager.activeSceneChanged -= OnSceneChange;
    }


    private void OnApplicationFocus(bool focus)
    {
      if (enabled)
      {
        if (focus)
        {
          playerControls.Enable();
        }
        else
        {
          playerControls.Disable();
        }
      }
    }
    private void Update()
    {
      HandleMovementInput();
      HandleCameraMovementInput();
    }

    private void HandleMovementInput()
    {
      verticalInput = movementInput.y;
      horizontalInput = movementInput.x;

      // RETURNS THE ABSOLUTE NUMBER, (Meaning number without the negative sign, so its always positive)
      moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

      // WE CLAMP THE VALUES, SO THEY ARE 0, 0.5 OR 1 (OPTIONAL)
      if (moveAmount <= 0.5f && moveAmount > 0)
      {
        moveAmount = 0.5f;
      }
      else if (moveAmount > 0.5f && moveAmount <= 1)
      {
        moveAmount = 1;
      }

      if (player == null) return;
     

      player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
    }

    private void HandleCameraMovementInput()
    {
      cameraVerticalInput = cameraInput.y;
      cameraHorizontalInput = cameraInput.x;
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