using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonGeneric<GameManager>
{
    public GameObject GameOverPanel;
    //public bool IsGameOver;
   public void EnableGameOverPanel()
    {       
        GameOverPanel.SetActive(true);
    }
}
