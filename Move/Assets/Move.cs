using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform childTransform;
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        childTransform.localPosition = new Vector3(0, 2, 0);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        childTransform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // ���� ����Ű�� ������ �ʴ� (0, 1, 0)�� �ӵ��� �����̵�
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // ���� ����Ű�� ������
            // �ڽ��� �ʴ� (0, 0, 180) ȸ��
            transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
            // �ڽ��� �ʴ� (0, 180, 0) ȸ��
            childTransform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);
            childTransform.Rotate(new Vector3(0, -180, 0) * Time.deltaTime);
        }
    }
}
