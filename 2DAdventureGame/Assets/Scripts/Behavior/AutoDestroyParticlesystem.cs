﻿using UnityEngine;
using System.Collections;

public class AutoDestroyParticlesystem : MonoBehaviour {

    private ParticleSystem _particleSystem;

    public void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void Update()
    {
        if (_particleSystem.isPlaying)
            return;
        Destroy(gameObject);
    }
}
