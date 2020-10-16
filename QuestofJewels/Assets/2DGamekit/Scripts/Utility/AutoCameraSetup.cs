using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Gamekit2D
{
    public class AutoCameraSetup : MonoBehaviour
    {
        public bool autoSetupCameraFollow = true;
        public string cameraFollowGameObjectName = "Ellen";
        CinemachineVirtualCamera cam;
        public CinemachineFramingTransposer camTrans;
        float defaultScreenXPos;
        public float screenXRightPos;
        public float screenXLeftPos;

        void Awake ()
        {
            if(!autoSetupCameraFollow)
                return;

            cam = GetComponent<CinemachineVirtualCamera> ();
            camTrans = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
            defaultScreenXPos = camTrans.m_ScreenX;

            if (cam == null)
                throw new UnityException("Virtual Camera was not found, default follow cannot be assigned.");

            //we manually do a "find", because otherwise, GameObject.Find seem to return object from a "preview scene" breaking the camera as the object is not the right one
            var rootObj = gameObject.scene.GetRootGameObjects();
            GameObject cameraFollowGameObject = null;
            foreach (var go in rootObj)
            {
                if (go.name == cameraFollowGameObjectName)
                    cameraFollowGameObject = go;
                else
                {
                    var t = go.transform.Find(cameraFollowGameObjectName);
                    if (t != null)
                        cameraFollowGameObject = t.gameObject;
                }

                if (cameraFollowGameObject != null) break;
            }
        
            if(cameraFollowGameObject == null)
                throw new UnityException("GameObject called " + cameraFollowGameObjectName + " was not found, default follow cannot be assigned.");

            if (cam.Follow == null)
            {
                cam.Follow = cameraFollowGameObject.transform;
            }
        }

        void Start()
        {
            screenXRightPos = 0.13f;
            screenXLeftPos = 0.85f;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                camTrans.m_ScreenX = screenXRightPos;
            }
            else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            {
                camTrans.m_ScreenX = screenXLeftPos;
            }
            
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                camTrans.m_ScreenX = defaultScreenXPos;
            }
        }
    }
}