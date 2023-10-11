using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }      //�̱��� ȣ�� static < �������� �޸� �÷���

    private void Awake()                            //Awake �� ����� ��
    {
        if(Instance ==null)                         //�ش� instance�� �������� ������
        {
            Instance = this;                        //�ν��Ͻ��� �� Ŭ���� -> class Singleton
            DontDestroyOnLoad(gameObject);          //����Ƽ�� �ı����� �ʴ� ��ü�� ����
        }
        else
        {
            Destroy(gameObject);                    //�ش� �ν��Ͻ��� �����ϸ� �������ڸ��� �ı��Ѵ�
        }
    }

    public int playerScore = 0;                     //�÷��̾� ���ھ� ���

    public void IncreaseScore(int amount)           //�Լ��� ���ؼ� ���������ش�
    {
        playerScore += amount;
    }

}
