using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationAngle = 60f;
 

    void Update()
    {
        /*
         * 1�ʴ� rotationAngle��ŭ ȸ���ϵ��� Time.deltaTime(�ʴ� �����ӿ� ������ ���� ��)�� �����ش�.
         * ����, 60FPS ��ǻ�Ͷ�� 
         * rotationAngle * (1/60) * (1�ʿ� 60�� update() �Լ� ����)  = �� 60�� ȸ��
         */
        transform.Rotate(0f, rotationAngle * Time.deltaTime , 0f);
    }
}
