using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class GestureControl : MonoBehaviour

    {
        // set var
        public float distance = 10.0f;
        
        public float scaleFactor = 4f;


        public float maxDistance = 0f;
        public float minDistance = -50f;


        
        private Vector2 oldPosition1;
        private Vector2 oldPosition2;


        private Vector2 lastSingleTouchPosition;

        private Vector3 m_CameraOffset;
        private Camera m_Camera;

        public bool useMouse = true;

        
        public float xMin = -10;
        public float xMax = 50;
        public float zMin = -30;
        public float zMax = 10;

        
        private bool m_IsSingleFinger;

        
        void Start()
        {
            m_Camera = this.GetComponent<Camera>();
            m_CameraOffset = m_Camera.transform.position;
        }

        void Update()
        {
            
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
                {
                    
                    lastSingleTouchPosition = Input.GetTouch(0).position;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved && m_IsSingleFinger)
                {
                    MoveCamera(Input.GetTouch(0).position);
                }
                m_IsSingleFinger = true;

            }
            else if (Input.touchCount > 1)
            {
                
                if (m_IsSingleFinger)
                {
                    oldPosition1 = Input.GetTouch(0).position;
                    oldPosition2 = Input.GetTouch(1).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    ScaleCamera();
                }

                m_IsSingleFinger = false;
            }


             
            if (useMouse)
            {
                distance -= Input.GetAxis("Mouse ScrollWheel") * scaleFactor;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
                if (Input.GetMouseButtonDown(0))
                {
                    lastSingleTouchPosition = Input.mousePosition;
                    Debug.Log("GetMouseButtonDown:" + lastSingleTouchPosition);
                }
                if (Input.GetMouseButton(0))
                {
                    MoveCamera(Input.mousePosition);
                }
            }


        }

         
        private void ScaleCamera()
        {
            
            var tempPosition1 = Input.GetTouch(0).position;
            var tempPosition2 = Input.GetTouch(1).position;


            float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
            float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

            
            distance -= (currentTouchDistance - lastTouchDistance) * scaleFactor * Time.deltaTime;


             
            distance = Mathf.Clamp(distance, minDistance, maxDistance);


            
            oldPosition1 = tempPosition1;
            oldPosition2 = tempPosition2;
        }


        
        private void LateUpdate()
        {
            var position = m_CameraOffset + m_Camera.transform.forward * -distance;
            m_Camera.transform.position = position;
        }


        private void MoveCamera(Vector3 scenePos)
        {
        if (Input.touchCount == 1)
        {
            Vector3 lastTouchPostion = m_Camera.ScreenToWorldPoint(new Vector3(lastSingleTouchPosition.x, lastSingleTouchPosition.y, -1));
            Vector3 currentTouchPosition = m_Camera.ScreenToWorldPoint(new Vector3(scenePos.x, scenePos.y, -1));

            Vector3 v = currentTouchPosition - lastTouchPostion;
            m_CameraOffset += new Vector3(v.x, 0, v.z) * m_Camera.transform.position.y;

            
            m_CameraOffset = new Vector3(Mathf.Clamp(m_CameraOffset.x, xMin, xMax), m_CameraOffset.y, Mathf.Clamp(m_CameraOffset.z, zMin, zMax));
            //Debug.Log(lastTouchPostion + "|" + currentTouchPosition + "|" + v);  
            lastSingleTouchPosition = scenePos;
        }
            
        }
    }
