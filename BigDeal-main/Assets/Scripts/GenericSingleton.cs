using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();  //�ش� Ÿ�� ã��

                if (_instance == null)
                {
                    GameObject obj = new GameObject();  //��ü �����    
                    obj.name = typeof(T).Name;          //������Ʈ �̸� ����
                    _instance = obj.AddComponent<T>();  //�ν��Ͻ� ������Ʈ ���δ�.
                }
            }
            return _instance;
        }
         

    }
    public virtual void Awake()                            //Awake �� ����� ��
    {
        if (_instance == null)                         //�ش� instance�� �������� ������
        {
            _instance = this as T;                        //�ν��Ͻ��� �� Ŭ���� -> class Singleton
            DontDestroyOnLoad(gameObject);          //����Ƽ�� �ı����� �ʴ� ��ü�� ����
        }
        else if(_instance != this)
        {
            Destroy(gameObject);                    //�ش� �ν��Ͻ��� �����ϸ� �������ڸ��� �ı��Ѵ�
        }
    }
}
