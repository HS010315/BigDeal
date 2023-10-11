using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericContainerExample : MonoBehaviour
{
    private GenericContainer<int> intContainer;
    private GenericContainer<string> stringContainer;

    private void Start()
    {
        intContainer = new GenericContainer<int>(5);
        stringContainer = new GenericContainer<string>(5);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            string randomString = "Item " + Random.Range(1, 10);
            intContainer.Add(Random.Range(1, 100)); //컨테이너에 더한다
            DisplayContainerItems(intContainer);    //디버그에 보여줌
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string randomString = "Item " + Random.Range(1, 10);
            stringContainer.Add(randomString);      //컨테이너에 더한다
            DisplayContainerItems(stringContainer);    //디버그에 보여줌
        }
    }
    private void DisplayContainerItems<T>(GenericContainer<T> container)
    {
        T[] items = container.GetItems();
        string temp = "";

        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                temp += items[i].ToString() + " - ";

            }
            else 
            { 
                temp += " Empty - ";
            }
        }
        Debug.Log(temp);
    }
}
