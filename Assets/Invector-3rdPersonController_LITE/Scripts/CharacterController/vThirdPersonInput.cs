using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticalInput = "Vertical";
        /*public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;*/

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [Header("Android Input")]
        public Joystick joystick;
        public Button sprintButton;

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public CameraFollow tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion

        protected virtual void Start()
        {
            InitializeController();
            InitializeTpCamera();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitializeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<CameraFollow>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.target = this.transform;
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            //StrafeInput();
            //JumpInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = joystick.Horizontal;
            cc.input.z = joystick.Vertical;
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        /*protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }*/

        public virtual void SprintInput()
        {
            cc.Sprint(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerPress == sprintButton.gameObject)
            {
                cc.Sprint(true);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerPress == sprintButton.gameObject)
            {
                cc.Sprint(false);
            }
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        /*protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();
        }*/

        #endregion       
    }
}