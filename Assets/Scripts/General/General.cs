﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class General : MonoBehaviour {

    public Font font;
    private Slider slider;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        slider = FindObjectOfType<Slider>();
        slider.maxValue = 0;
        foreach (Text text in FindObjectsOfType<Text>())
            text.font = font;
            
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
            Zombie.aliveZombies.Clear();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else audioSource.Play();
        }
            
	}

    public static void MakeSmaller(GameObject gameObject)
    {
        gameObject.transform.localScale *= 0.9f;
    }
}
