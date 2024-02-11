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
        // 화면 터치했을 때만 TrailRender 켜기
        if (Input.GetMouseButton(0))
        {
            trailRender.SetActive(true);
        }

        else trailRender.SetActive(false);

        var centerRay = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        // 화면 중심에서 AR 레이저를 쏜다.
        arRaycastManager.Raycast(centerRay, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);


        // 레이저가 인식한 지면에 닿았다.
        if (hits.Count > 0 )
        {
            // 닿은 위치에 Indicator 표시
            indicator.SetActive(true);
            indicator.transform.position = hits[0].pose.position;
            indicator.transform.rotation = hits[0].pose.rotation;
        }

        // 아무것도 인식 하지 못할 때
        else
        {
            indicator.SetActive(false);
        }
        
    }
}
