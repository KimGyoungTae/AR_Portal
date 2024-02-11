using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;


public class GPSManager : MonoBehaviour
{
    public Text text_UI;
    public GameObject PopUP;    // 팝업 받아오기..
    public bool isFirst = false; // 팝업을 한 번만 띄우기 위한 bool 타입 변수

    public bool isExitPopUP = false;

    public double[] lats;   // 위도 
    public double[] longs;  // 경도
    public float assumedHeight = -0.5f;

    private double myLat = 0.0;
    private double myLong = 0.0;

    public GameObject arObject;
    public GameObject slender;

    Vector3 SlenderPosition = new Vector3(1, 0, 0);
    public GPSProjection gpsProjection;

    IEnumerator Start()
    {
        // 안드로이드 인 경우에는 위치 정보를 가져오는 권한 허용을 해야함..
        while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
            // 권한 허용을 안 했을 시 계속 요청(Request) 을 하도록... 
            Permission.RequestUserPermission(Permission.FineLocation);
        }


        // GPS를 켰는지 안 켰는지 확인 -> 안 켰을 경우 종료..
        if (!Input.location.isEnabledByUser)
            yield break;

        // 켜져 있으면 위치 정보를 받아옴..
        Input.location.Start(10, 1);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Failed 인 경우
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }

        // Running 인 경우 / 동작을 할 때
        else
        {
            // 위도 / 경도 / 고도
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);


            while (true)
            {
                yield return null;

                // 위도 & 경도만 UI에 보이게 만듬.. 
                // while 문 안에서 위도 & 경도 갱신
                text_UI.text = Input.location.lastData.latitude + " / " + Input.location.lastData.longitude;
            }
        }


        // 계속 위치정보를 가져올 때는 주석 처리해야 함..
       // Input.location.Stop();
    }

    void Update()
    {
        if(Input.location.status == LocationServiceStatus.Running)
        {
             myLat = Input.location.lastData.latitude;
             myLong = Input.location.lastData.longitude;

           OnActivePopUP(myLat, myLong);
          //  AfterPopUP(myLat, myLong);
        }

    }

    private void OnActivePopUP(double curLat, double curLong)
    {
        double remainDistance = distance(curLat, curLong, lats[0], longs[0]);

        if (remainDistance <= 15f) // 15m 안으로 들어왔을 때
        {
            if (!isFirst)   // isFirst false일 때
            {
                isFirst = true;
                PopUP.SetActive(true);

            }
        }
    }

    // 팝업 창을 닫은 이후 오브젝트의 위치를 갱신시킨다.
    private (double, double) AfterPopUP(double lat, double lon)
    {
       
        //Vector3 newDirection = direction(lat, lon, lats[1], longs[1]);
        //SlenderPosition = SetPosition(newDirection, newDistace);

       return (lat, lon);
    }

    public void ShowObject()
    {
        PopUP.SetActive(false);
        isExitPopUP = true;

        double newDis = distance(myLat, myLong, lats[1], longs[1]);

        if (isExitPopUP == true)
        {
            Debug.Log("팝업 닫아짐..");
            SlenderPosition = gpsProjection.directionProjection(myLat, myLong, lats[1], longs[1], newDis);
            CreateSpawn(SlenderPosition);
        }


        arObject.SetActive(true);
    
        //// 휴대폰 기준 정면 3m 떨어진 곳에 오브젝트 생성
        //arObject.transform.position = Camera.main.transform.forward * 10f;

        // 휴대폰 기준 정면 10m 떨어진 곳에 오브젝트 생성하면서 Y값을 -0.5로 조절
        Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * 10f;
        newPosition.y = -0.5f; // Y값을 -0.5로 설정
        arObject.transform.position = newPosition;


    }


    private void CreateSpawn(Vector3 newPosition)
    {
      //  Instantiate(slender, newPosition, Quaternion.identity);

        slender.SetActive(true);
        slender.transform.position = newPosition;
        Debug.Log(newPosition);
    }

   
    public Vector3 SetPosition(Vector3 GetDir, double Distance)
    {
        // 새로운 위치 = 현재 오브젝트 위치 + ( 방향 벡터 * 거리 )
        Vector3 SlenderPosition = slender.transform.position + GetDir * (float)Distance;
        
        return SlenderPosition;

        
    }


    // 위도와 경도를 받아와 방향 벡터 반환
    public Vector3 direction(double lat1, double lon1, double lat2, double lon2)
    {

        Vector3 directionVector = new Vector3((float)(lon2 - lon1), 0f, (float)(lat2 - lat1)).normalized;

        return directionVector;
    }



    // 지표면 거리 계산 공식(하버사인 공식)
    public double distance(double lat1, double lon1, double lat2, double lon2)
    {
        double theta = lon1 - lon2;

        double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

        dist = Math.Acos(dist);

        dist = Rad2Deg(dist);

        dist = dist * 60 * 1.1515;

        dist = dist * 1609.344; // 미터 변환

        return dist;    // 미터 반환
    }

    private double Deg2Rad(double deg)
    {
        return (deg * Mathf.PI / 180.0f);
    }

    private double Rad2Deg(double rad)
    {
        return (rad * 180.0f / Mathf.PI);
    }

    
}
