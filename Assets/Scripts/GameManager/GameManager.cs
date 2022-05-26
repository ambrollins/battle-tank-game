using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    //public bool IsGameOver;
   public void EnableGameOverPanel()
    {       
        GameOverPanel.SetActive(true);
    }
}
