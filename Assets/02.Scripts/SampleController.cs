using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    void Start()
    {
        Singleton.Instance.IncreaseScore(10);
        //start �Լ��� ȣ��� �� Instance�� �����ؼ� 10���� �߰�
        GameManager.instance.IncreaseScore(15);
    } 


}
