using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    void Start()
    {
        Singleton.Instance.IncreaseScore(10);
        //start 함수가 호출될 때 Instance에 접근해서 10점을 추가
        GameManager.instance.IncreaseScore(15);
    } 


}
