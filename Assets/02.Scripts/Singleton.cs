using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }      //싱글톤 호출 static < 전역으로 메모리 올려줌

    private void Awake()                            //Awake 가 실행될 때
    {
        if(Instance ==null)                         //해당 instance가 존재하지 않으면
        {
            Instance = this;                        //인스턴스는 이 클래스 -> class Singleton
            DontDestroyOnLoad(gameObject);          //유니티에 파괴되지 않는 객체로 통제
        }
        else
        {
            Destroy(gameObject);                    //해당 인스턴스가 존재하면 생성하자마자 파괴한다
        }
    }

    public int playerScore = 0;                     //플레이어 스코어 등록

    public void IncreaseScore(int amount)           //함수를 통해서 증가시켜준다
    {
        playerScore += amount;
    }

}
