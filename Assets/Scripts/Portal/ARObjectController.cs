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

            // ������Ʈ�� null�� �� ������Ʈ ����
            if(!arObject)
            {
                arObject = Instantiate(placePrefab, hitPose.position, hitPose.rotation);
            }

            // �̹� �����Ǿ��ٸ�, �ٽ� ��ġ�� ��ġ�� �̵��� ���ֱ�
            else
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
