using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        Invoke("StartGame1", 0.4f);
        
    }

    public void StartGame1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
