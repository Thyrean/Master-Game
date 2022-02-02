#region using

using UnityEditor;
using UnityEngine;

#endregion

namespace Helpers
{
    /// <summary>
    ///     Script that realized mouse based movement for the camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary> Maximal distante to the target. </summary>
        private static readonly float m_DistanceMax = 15f;

        /// <summary> Minimal distance to the target. </summary>
        private readonly float m_DistanceMin = .5f;

        /// <summary> camera movement speed on the x Achsis </summary>
        private readonly float m_XSpeed = 100.0f;

        /// <summary> camera ofset position limit on the x achsis </summary>
        private readonly float m_YMaxLimit = 80f;

        /// <summary> camera ofset position limit on the y achsis </summary>
        private readonly float m_YMinLimit = -20f;

        /// <summary> camera movement speed on the y Achsis </summary>
        private readonly float m_YSpeed = 100.0f;


        /// <summary> Current distance to the camera. </summary>
        private float m_Distance = 5.0f;

        /// <summary> current x position </summary>
        private float m_X;

        /// <summary> current y position </summary>
        private float m_Y;

        /// <summary> Camera Transform that should be looked at. </summary>
        public Transform target;


        /// <summary>
        ///     Init the start position.
        /// </summary>
        private void Start()
        {
            var angles = transform.eulerAngles;
            m_X = angles.y;
            m_Y = angles.x;
        }

        /// <summary>
        ///     Validates Mouse input and translates it to position and rotation,
        ///     so the camera is always facing the target object.
        /// </summary>
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var target = GetTarget();
                if (target == this.target)
                {
                    this.target = null;
#if UNITY_EDITOR
                    Selection.activeGameObject = null;
#endif
                }
                else if (target != null)
                {
                    this.target = target;
#if UNITY_EDITOR
                    Selection.activeGameObject = target.gameObject;
#endif
                }
            }

            if (target)
                OrbitAroundTarget();
            else
                MoveByInput();
            lastMouse = Input.mousePosition;
        }

        public Transform GetTarget()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out var hit, 500) ? hit.transform : null;
        }


        #region orbit

        private void OrbitAroundTarget()
        {
            if (Input.GetMouseButton(0))
            {
                m_X += Input.GetAxis("Mouse X") * m_XSpeed * m_Distance * 0.02f;
                m_Y -= Input.GetAxis("Mouse Y") * m_YSpeed * 0.02f;
            }

            m_Y = ClampAngle(m_Y, m_YMinLimit, m_YMaxLimit);

            var rotation = Quaternion.Euler(m_Y, m_X, 0);

            m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * 5, m_DistanceMin, m_DistanceMax);

            /*if (Physics.Linecast(m_Target.position, transform.position, out var hit) && hit.transform == m_Target)
                m_Distance -= hit.distance;*/
            var negDistance = new Vector3(0.0f, 0.0f, -m_Distance);
            var position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            if (Input.GetKey(KeyCode.Backspace))
            {
                target.gameObject.SetActive(false);
                target = null;
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        #endregion

        #region inputBased

        private float mainSpeed = 100.0f; //regular speed
        private float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
        private float maxShift = 1000.0f; //Maximum speed when holdin gshift
        private float camSens = 0.25f; //How sensitive it with mouse

        private Vector3
            lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)

        private float totalRun = 1.0f;

        private void MoveByInput()
        {
            if (Input.GetMouseButton(0))
            {
                lastMouse = Input.mousePosition - lastMouse;
                lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
                lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y,
                    0);
                transform.eulerAngles = lastMouse;
                //Mouse  camera angle done.  
            }

            //Keyboard commands
#pragma warning disable CS0219 // Die Variable "f" ist zugewiesen, ihr Wert wird aber nie verwendet.
            var f = 0.0f;
#pragma warning restore CS0219 // Die Variable "f" ist zugewiesen, ihr Wert wird aber nie verwendet.
            var p = GetBaseInput();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            var newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space))
            {
                //If player wants to move on X and Z axis only
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(p);
            }
        }

        private Vector3 GetBaseInput()
        {
            //returns the basic values, if it's 0 than it's not active.
            var p_Velocity = new Vector3();
            if (Input.GetKey(KeyCode.W)) p_Velocity += new Vector3(0, 0, 1);
            if (Input.GetKey(KeyCode.S)) p_Velocity += new Vector3(0, 0, -1);
            if (Input.GetKey(KeyCode.A)) p_Velocity += new Vector3(-1, 0, 0);
            if (Input.GetKey(KeyCode.D)) p_Velocity += new Vector3(1, 0, 0);
            return p_Velocity;
        }

        #endregion
    }
}