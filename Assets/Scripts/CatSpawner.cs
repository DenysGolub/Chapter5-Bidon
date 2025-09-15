using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CatSpawner : MonoBehaviour
{
    public GameObject playerAim;
    public GameObject catPrefab;
    public AudioSource audioSource;
    public Transform[] spawnPoints;
    private object[] particles;
    Random rnd;

    void Start()
    {
        particles = Resources.LoadAll("Particles", typeof(ParticleSystem));
        rnd = new Random();
        SpawnCat();
    }


    void OnEnable()
    {
        DisappearingCat.onCatDestroy += SpawnCat;
    }

    void OnDisable()
    {
        DisappearingCat.onCatDestroy -= SpawnCat;
    }

    void SpawnCat()
    {
        GameObject cat = catPrefab;
        ParticleSystem destroyParticle = (ParticleSystem)particles[rnd.Next(0, particles.Length)];
        var positionIndex = new Random().Next(0, spawnPoints.Length);

        cat.transform.position = spawnPoints[positionIndex].transform.position;

        DisappearingCat dc = cat.GetComponent<DisappearingCat>();
    
        dc.destroyEffect = destroyParticle;
        dc.minDistanceForDisappering = rnd.Next(3, 7);
        dc.playerAim = playerAim;
        dc.audioSource = audioSource;

        Instantiate(catPrefab);
        
    }
    


    
}
