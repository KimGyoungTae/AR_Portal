// 사용자와 Portal와 충돌 판정과 판정에 대한 반응에 대한 코드 -> 실패한 코드

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public Transform APos;
    public Transform BPos;

    public GameObject PortalA;
    public GameObject PortalB;
    public GameObject Env;

    //public static event Action<bool> OnChange;
    //private bool _isChange;

    public bool isChange = false;
    public static PortalManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        //PortalA.SetActive(true);
        //PortalB.SetActive(false);
        //Env.SetActive(false);
    }

   
  

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal A"))
        {
            
            Camera.main.transform.position = BPos.transform.position;

            // AR 카메라의 Rotation의 Y값을 -180으로
            //   Vector3 newRotation = new Vector3(0f, -180f, 0f); // Y값을 -180으로 변경

           // Camera.main.transform.rotation = new Quaternion(transform.rotation.x, BPos.transform.rotation.y, transform.rotation.z, transform.rotation.w);

            Debug.Log("부딫힘"); // Rigidbody를 추가해야 함..


            // Portal A는 사라지고, Portal B와 가상환경 On 시키는 코드 추가해야함.
            // Env.SetActive(true);

            PortalA.SetActive(false);
            isChange = true;
           // PortalB.SetActive(true);


            // Portal A View가 사라지는 문제 -> 카메라로 비출 물체가 없어서 그런거 같음..
            // B Cam을 살릴 수 있는 방안이 필요해보임..

        }


        if (other.CompareTag("Portal B"))
        {
            Camera.main.transform.position = APos.transform.position;

          //  Camera.main.transform.rotation = new Quaternion(transform.rotation.x, APos.transform.rotation.y, transform.rotation.z, transform.rotation.w);


            // Portal B와 가상환경은 사라지고, Portal A가 다시 나타나야 함..
         //   Env.SetActive(false);
            PortalA.SetActive(true);
            PortalB.SetActive(false);

            isChange = true;
        }
    }


}
