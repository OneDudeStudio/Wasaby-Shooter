using BeatDetection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;


class audio1 : MonoBehaviour
{
    private int sampleRate;
    [SerializeField] private AudioSource audioSource;
    private double trackLength = 0;

    int numChannels;
    int numTotalSamples;
    float clipLength;
    float[] multiChannelSamples;

    List<DetectedBeat> beats;

    int counter = 0;
    float songStartTime;


    private bool isShouldPlay = false;

    BeatDetectionV2 det;

    private void Start()
    {
        Detect();
    }


    private void Update()
    {
        if (isShouldPlay)
        {
            isShouldPlay = false;

            audioSource.Play();
            songStartTime = (float)AudioSettings.dspTime;
        }
        if (!audioSource.isPlaying)
        {
            return;
        }

        //Debug.Log(TimeSpan.FromSeconds(Time.time-songStartTime));
        //Debug.Log(beats[counter].TimeOffset);

        if (TimeSpan.FromSeconds((float)AudioSettings.dspTime - songStartTime) > beats[counter].TimeOffset)
        {
            StartCoroutine(show());
            counter++;
        }
    }

    [SerializeField] private Transform sphere;
    private Vector3 scale = Vector3.one * 2;
    private IEnumerator show()
    {
        sphere.localScale = scale;
        yield return new WaitForSeconds(.1f);
        sphere.localScale = Vector3.one;
    }


    private void Detect()
    {
        sampleRate = audioSource.clip.frequency;
        multiChannelSamples = new float[audioSource.clip.samples * audioSource.clip.channels];
        numChannels = audioSource.clip.channels;
        numTotalSamples = audioSource.clip.samples;
        clipLength = audioSource.clip.length;

        audioSource.clip.GetData(multiChannelSamples, 0);

        det = new BeatDetectionV2(sampleRate, multiChannelSamples.ToList());


        Thread bgThread = new Thread(Det);


        //Thread bgThread = new Thread(Detect);

        Debug.Log("Starting Background Thread");
        bgThread.Start();
    }

    private void Det()
    {
        det.Detect();
        beats = det.GetBeats();
        isShouldPlay = true;
    }
}