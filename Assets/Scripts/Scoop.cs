using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoop : MonoBehaviour
{
    public int health = 5;
    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        if (health<0)
		{
            this.gameObject.SetActive(false);

		}
    }

	internal void RegisterHit()
	{
        health -= 1;
        particles?.Play();

    }
}
