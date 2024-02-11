using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectController : MonoBehaviour
{
    [SerializeField]
    public GameObject placePrefab;

    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject arObject;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon ))
        {
            var hitPose = hits[0].pose;

            // 오브젝트가 null일 때 오브젝트 생성
            if(!arObject)
            {
                arObject = Instantiate(placePrefab, hitPose.position, hitPose.rotation);
            }

            // 이미 생성되었다면, 다시 터치한 위치에 이동만 해주기
            else
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
