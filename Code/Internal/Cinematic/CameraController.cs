using System;
using System.Linq;
using LeTai;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Cinematic
{
    public class CameraController : MonoBehaviour
    {
        public Camera Camera;
        [Range(0, 4)]
        public float MoveSpeed;
        [Range(0, 4)] 
        public float ZoomSpeed;
        [Range(0, 4)]
        public float RotateSpeed;

        private bool rotate;
        private bool boost;
        
        private void Start()
        {
            InputManager.Instance.RegisterInputAction("CameraLook", ctx =>
            {
                rotate = ctx.performed;
            });
            InputManager.Instance.RegisterInputAction("Boost", ctx =>
            {
                boost = ctx.performed;
            });
        }

        private void Update()
        {
            var moveVal = InputManager.Instance.MainPlayerInput.actions["Move"].ReadValue<Vector2>();
            var zoomVal = InputManager.Instance.MainPlayerInput.actions["Scroll"].ReadValue<Vector2>();
            var lookVal = InputManager.Instance.MainPlayerInput.actions["Look"].ReadValue<Vector2>();
            var finalMoveSpeed = MoveSpeed;
            if (boost) finalMoveSpeed *= 2;
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, Camera.transform.position +
                new Vector3(moveVal.x * finalMoveSpeed, 0, moveVal.y * finalMoveSpeed) + Camera.transform.forward * (zoomVal.y * ZoomSpeed) / 10,
                Time.deltaTime * 10);
            if (rotate)
            {
                Camera.transform.rotation = Quaternion.Euler(Camera.transform.rotation.eulerAngles + new Vector3(-lookVal.y, lookVal.x, 0) * RotateSpeed);
            }
        }
    }
}