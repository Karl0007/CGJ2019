using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 此脚本是能够将文本字符串随着时间打字或褪色显示。
/// </summary>

[RequireComponent(typeof(Text))]
[AddComponentMenu("Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
    public UnityEvent myEvent;
    public int charsPerSecond = 0;
    // public AudioClip mAudioClip;             // 打字的声音，不是没打一个字播放一下，开始的时候播放结束就停止播放
    private bool isActive = false;

    private float timer;
    private string words;
    private Text mText;

    void Start()
    {
        if (myEvent == null)
            myEvent = new UnityEvent();

        words = GetComponent<Text>().text;
        GetComponent<Text>().text = string.Empty;
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(1, charsPerSecond);
        mText = GetComponent<Text>();
    }

    void ReloadText()
    {
        timer = 0;
        words = GetComponent<Text>().text;
        mText = GetComponent<Text>();
    }

    public void OnStart()
    {
        ReloadText();
        isActive = true;
    }

    void OnStartWriter()
    {
        if (isActive)
        {
            try
            {
                
                mText.text = words.Substring(0, (int)(charsPerSecond * timer));
                timer += Time.deltaTime;
            }
            catch (Exception)
            {
                OnFinish();
            }
        }
    }

    void OnFinish()
    {
        isActive = false;
        timer = 0;
        GetComponent<Text>().text = words;
        try
        {
            myEvent.Invoke();
        }
        catch (Exception)
        {
            Debug.Log("问题");
        }
    }

    void Update()
    {
        OnStartWriter();
    }
}