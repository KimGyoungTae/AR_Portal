using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMagic : MonoBehaviour
{
    public GameObject magicWand;

    private bool checkMagic = false;

    private void Update()
    {

        // 마우스 버튼을 누를 때
        if(Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane + 0.1f;

            var screenPos = Camera.main.ScreenToWorldPoint(mousePos);

            magicWand.transform.LookAt(screenPos);

            checkMagic = true;
            Debug.Log(checkMagic);
        }

        // 버튼을 누르고 딱 마우스를 때는 순간
        else if (Input.GetMouseButtonUp(0))
        {
            checkMagic = false;
            Debug.Log(checkMagic);

        }
       
    }
}
