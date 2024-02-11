using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ARIndicator : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject indicator;

    public GameObject trailRender;

    void Update()
    {
        // ȭ�� ��ġ���� ���� TrailRender �ѱ�
        if (Input.GetMouseButton(0))
        {
            trailRender.SetActive(true);
        }

        else trailRender.SetActive(false);

        var centerRay = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        // ȭ�� �߽ɿ��� AR �������� ���.
        arRaycastManager.Raycast(centerRay, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);


        // �������� �ν��� ���鿡 ��Ҵ�.
        if (hits.Count > 0 )
        {
            // ���� ��ġ�� Indicator ǥ��
            indicator.SetActive(true);
            indicator.transform.position = hits[0].pose.position;
            indicator.transform.rotation = hits[0].pose.rotation;
        }

        // �ƹ��͵� �ν� ���� ���� ��
        else
        {
            indicator.SetActive(false);
        }
        
    }
}
