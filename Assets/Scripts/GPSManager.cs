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
    public GameObject PopUP;    // �˾� �޾ƿ���..
    public bool isFirst = false; // �˾��� �� ���� ���� ���� bool Ÿ�� ����

    public bool isExitPopUP = false;

    public double[] lats;   // ���� 
    public double[] longs;  // �浵
    public float assumedHeight = -0.5f;

    private double myLat = 0.0;
    private double myLong = 0.0;

    public GameObject arObject;
    public GameObject slender;

    Vector3 SlenderPosition = new Vector3(1, 0, 0);
    public GPSProjection gpsProjection;

    IEnumerator Start()
    {
        // �ȵ���̵� �� ��쿡�� ��ġ ������ �������� ���� ����� �ؾ���..
        while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
            // ���� ����� �� ���� �� ��� ��û(Request) �� �ϵ���... 
            Permission.RequestUserPermission(Permission.FineLocation);
        }


        // GPS�� �״��� �� �״��� Ȯ�� -> �� ���� ��� ����..
        if (!Input.location.isEnabledByUser)
            yield break;

        // ���� ������ ��ġ ������ �޾ƿ�..
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

        // Failed �� ���
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }

        // Running �� ��� / ������ �� ��
        else
        {
            // ���� / �浵 / ��
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);


            while (true)
            {
                yield return null;

                // ���� & �浵�� UI�� ���̰� ����.. 
                // while �� �ȿ��� ���� & �浵 ����
                text_UI.text = Input.location.lastData.latitude + " / " + Input.location.lastData.longitude;
            }
        }


        // ��� ��ġ������ ������ ���� �ּ� ó���ؾ� ��..
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

        if (remainDistance <= 15f) // 15m ������ ������ ��
        {
            if (!isFirst)   // isFirst false�� ��
            {
                isFirst = true;
                PopUP.SetActive(true);

            }
        }
    }

    // �˾� â�� ���� ���� ������Ʈ�� ��ġ�� ���Ž�Ų��.
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
            Debug.Log("�˾� �ݾ���..");
            SlenderPosition = gpsProjection.directionProjection(myLat, myLong, lats[1], longs[1], newDis);
            CreateSpawn(SlenderPosition);
        }


        arObject.SetActive(true);
    
        //// �޴��� ���� ���� 3m ������ ���� ������Ʈ ����
        //arObject.transform.position = Camera.main.transform.forward * 10f;

        // �޴��� ���� ���� 10m ������ ���� ������Ʈ �����ϸ鼭 Y���� -0.5�� ����
        Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * 10f;
        newPosition.y = -0.5f; // Y���� -0.5�� ����
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
        // ���ο� ��ġ = ���� ������Ʈ ��ġ + ( ���� ���� * �Ÿ� )
        Vector3 SlenderPosition = slender.transform.position + GetDir * (float)Distance;
        
        return SlenderPosition;

        
    }


    // ������ �浵�� �޾ƿ� ���� ���� ��ȯ
    public Vector3 direction(double lat1, double lon1, double lat2, double lon2)
    {

        Vector3 directionVector = new Vector3((float)(lon2 - lon1), 0f, (float)(lat2 - lat1)).normalized;

        return directionVector;
    }



    // ��ǥ�� �Ÿ� ��� ����(�Ϲ����� ����)
    public double distance(double lat1, double lon1, double lat2, double lon2)
    {
        double theta = lon1 - lon2;

        double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

        dist = Math.Acos(dist);

        dist = Rad2Deg(dist);

        dist = dist * 60 * 1.1515;

        dist = dist * 1609.344; // ���� ��ȯ

        return dist;    // ���� ��ȯ
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
