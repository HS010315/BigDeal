using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public int playerScore = 0;                     //플레이어 스코어 등록

    public void IncreaseScore(int amount)           //함수를 통해서 증가시켜준다
    {
        playerScore += amount;
    }
}
