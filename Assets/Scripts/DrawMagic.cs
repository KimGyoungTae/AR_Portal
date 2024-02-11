using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMagic : MonoBehaviour
{
    public GameObject magicWand;

    private bool checkMagic = false;

    private void Update()
    {

        // ���콺 ��ư�� ���� ��
        if(Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane + 0.1f;

            var screenPos = Camera.main.ScreenToWorldPoint(mousePos);

            magicWand.transform.LookAt(screenPos);

            checkMagic = true;
            Debug.Log(checkMagic);
        }

        // ��ư�� ������ �� ���콺�� ���� ����
        else if (Input.GetMouseButtonUp(0))
        {
            checkMagic = false;
            Debug.Log(checkMagic);

        }
       
    }
}
