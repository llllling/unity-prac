using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationAngle = 60f;
 

    void Update()
    {
        /*
         * 1초당 rotationAngle만큼 회전하도록 Time.deltaTime(초당 프레임에 역수를 취한 값)을 곱해준다.
         * 만약, 60FPS 컴퓨터라면 
         * rotationAngle * (1/60) * (1초에 60번 update() 함수 실행)  = 총 60도 회전
         */
        transform.Rotate(0f, rotationAngle * Time.deltaTime , 0f);
    }
}
