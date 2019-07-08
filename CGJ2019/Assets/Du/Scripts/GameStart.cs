using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonT<PackageManager>.Instance.Clear();
    }

    public void StartGame()
    {
		SingletonT<PackageManager>.Instance.Clear();
		Invoke("StartGame1", 0.4f);
        
    }

    public void StartGame1()
    {
		SingletonT<PackageManager>.Instance.Clear();
		SceneManager.LoadScene("SampleScene");
    }
}
