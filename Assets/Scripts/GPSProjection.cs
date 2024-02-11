using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSProjection : MonoBehaviour
{
    // 위도와 경도를 받아온다. 그리고 투영좌표계로 변환한다. (EPSG 3857)
    // 원하는 벡터 연산을 한다. 
    // 그리고 다시 원래 위도 경도로 변환한다. (WGS 84)

    private static double currentMercatorCX = 0.0f;
    private static double currentMercatorCY = 0.0f;

    private static double currentLonX = 0.0f;
    private static double currentLatY = 0.0f;

   // public GPSManager manager;

    public Vector2 GPSPosition = new Vector2(0f, 0f);
    public Vector3 pos;

    private void Start()
    {
        pos = GPSEncoder.GPSToUCS(GPSPosition);

        print(pos);
        print(GPSEncoder.USCToGPS(pos));
    }

    void Projection()
    {
        //currentMercatorCX = lonToWebMercatorX(manager.longs[1]);
        //currentMercatorCY = latToWebMercatorY(manager.lats[1]);

        //currentLonX = webMercatorToLon(currentMercatorCX);
        //currentLatY = webMercatorToLat(currentMercatorCY);

        //Debug.Log(currentMercatorCX);
        //Debug.Log(currentMercatorCY);

        //Debug.Log(currentLonX);
        //Debug.Log(currentLatY);

    }


    // 현재 위도와 경도 그리고 목적지 위도와 경도를 받아온다.
    public Vector3 directionProjection(double lat1, double lon1, double lat2, double lon2, double dis)
    {
        // 현재 나의 위도와 경도를 투영좌표계 변환
        currentMercatorCX = lonToWebMercatorX(lon1);    // 경도
        currentMercatorCY = latToWebMercatorY(lat1);    // 위도

        // 목적지 위도와 경도를 투영좌표계 변환
        double destinationX = lonToWebMercatorX(lon2);
        double destinationY = latToWebMercatorY(lat2);

        // 투영좌표계 방향 구하기
        Vector3 directionVector = new Vector3((float)(destinationX - currentMercatorCX), 0f, (float)(destinationY - currentMercatorCY)).normalized;

        //   double GPSDistace = manager.distance(lat1, lon1, lat2, lon2);   // 거리
        double GPSDistace = dis;

        // 카메라 위치 기준 방향과 거리를 더해준다. 
        currentMercatorCX = Camera.main.transform.position.x + directionVector.x * (float)GPSDistace;   // 경도(x)
        currentMercatorCY = Camera.main.transform.position.z + directionVector.z * (float)GPSDistace;   // 위도(z)

        // 방향과 거리 적용 후 다시 위도 / 경도 변환
        currentLonX = webMercatorToLon(currentMercatorCX);
        currentLatY = webMercatorToLat(currentMercatorCY);


        return new Vector3((float)currentLonX, 0f, (float)currentLatY);
    }


    //WGS84 -> ESPG:3857  x
    public static double lonToWebMercatorX(double longitude)
    {
        return longitude * 20037508.34 / 180;
    }
    //WGS84 -> ESPG:3857  y
    public static double latToWebMercatorY(double latitude)
    {
        double y = Math.Log(Math.Tan((90 + latitude) * Math.PI / 360)) / (Math.PI / 180.0f);
        y = y * 20037508.34 / 180;
        return y;
    }

    //ESPG:3857 -> WGS84  x
    public static double webMercatorToLon(double x_3857)
    {
        return x_3857 * 180 / 20037508.34;
    }

    //ESPG:3857 -> WGS84  y
    public static double webMercatorToLat(double y_3857)
    {
        return Math.Atan(Math.Exp(y_3857 * Math.PI / 20037508.34)) * 360 / Math.PI - 90;
    }
}
