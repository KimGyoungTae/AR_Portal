// ����ڿ� Portal�� �浹 ������ ������ ���� ������ ���� �ڵ� -> ������ �ڵ�

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

            // AR ī�޶��� Rotation�� Y���� -180����
            //   Vector3 newRotation = new Vector3(0f, -180f, 0f); // Y���� -180���� ����

           // Camera.main.transform.rotation = new Quaternion(transform.rotation.x, BPos.transform.rotation.y, transform.rotation.z, transform.rotation.w);

            Debug.Log("�΋H��"); // Rigidbody�� �߰��ؾ� ��..


            // Portal A�� �������, Portal B�� ����ȯ�� On ��Ű�� �ڵ� �߰��ؾ���.
            // Env.SetActive(true);

            PortalA.SetActive(false);
            isChange = true;
           // PortalB.SetActive(true);


            // Portal A View�� ������� ���� -> ī�޶�� ���� ��ü�� ��� �׷��� ����..
            // B Cam�� �츱 �� �ִ� ����� �ʿ��غ���..

        }


        if (other.CompareTag("Portal B"))
        {
            Camera.main.transform.position = APos.transform.position;

          //  Camera.main.transform.rotation = new Quaternion(transform.rotation.x, APos.transform.rotation.y, transform.rotation.z, transform.rotation.w);


            // Portal B�� ����ȯ���� �������, Portal A�� �ٽ� ��Ÿ���� ��..
         //   Env.SetActive(false);
            PortalA.SetActive(true);
            PortalB.SetActive(false);

            isChange = true;
        }
    }


}
