using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
  public class PlayerCamera : MonoBehaviour
  {
    public static PlayerCamera instance;
    public PlayerManager player;
    public Camera cameraObject;
    [SerializeField] Transform cameraPivotTransform;

    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1;
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minimumPivot = -30;
    [SerializeField] float maximumPivot = 60;
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZPosition;
    private float targetCameraZPosition;


    private void Awake()
    {
      InitSingleton();
    }

    private void Start()
    {
      DontDestroyOnLoad(gameObject);
      cameraZPosition = cameraObject.transform.localPosition.z;
    }

    public void HandleAllCameraActions()
    {
      if (player != null)
      {
        HandleFollowTarget();
        HandleRotations();
      }
    }

    private void HandleFollowTarget()
    {
      Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
      transform.position = targetCameraPosition;
    }


    private void HandleRotations()
    {
      // ROTATE LEFT AND RIGHT BASED ON HORIZONTAL MOVEMENT ON THE RIGHT JOYSTICK
      leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
      // ROTATE UP AND DOWN BASED ON VERTICAL MOVEMENT ON THE RIGHT JOYSTICK
      upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
      // CLAMP THE UP AND DOWN LOOK ANGLE BETWEEN A MIN AND MAX VALUE
      upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

      Vector3 cameraRotation = Vector3.zero;
      Quaternion targetRotation;

      // ROTATE THIS GAMEOBJECT LEFT AND RIGHT
      cameraRotation.y = leftAndRightLookAngle;
      targetRotation = Quaternion.Euler(cameraRotation);
      transform.rotation = targetRotation;

      // ROTATE THE PIVOT GAMEOBJECT UP AND DOWN
      cameraRotation = Vector3.zero;
      cameraRotation.x = upAndDownLookAngle;
      targetRotation = Quaternion.Euler(cameraRotation);
      cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
      targetCameraZPosition = cameraZPosition;
      Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
      direction.Normalize();
      RaycastHit hit;

      if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
      {
        float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
        targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
      }

      if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
      {
        targetCameraZPosition = -cameraCollisionRadius;
      }

      cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
      cameraObject.transform.localPosition = cameraObjectPosition;
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
