using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource1;
    
    public float[] _samples = new float[64];
    public static float[] _freqBands = new float[8];

    public float[] _audioBand = new float[8];
    public float[] _bandBuffer = new float[8];
    public float[] _audioBandBuffer = new float[8];

    public float _audioProfile;

    float[] _bufferDecrease = new float[8];
    float[] _freqBandHighest = new float[8];

    public static float _amplitude, _amplitudeBuffer;
    float _amplitudeHighest;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GetSpectrumAudio();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBand();
        GetAmplitude();

    }

    private void GetSpectrumAudio()
    {
        audioSource1.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        int _count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            //int sampleCount = (8 * i);

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[_count] * (_count + 1);
                _count++;
            }

            average /= sampleCount;

            _freqBands[i] = average * 10;
        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBands[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBands[i];
                _bufferDecrease[i] = 0.005f;
            }

            if (_freqBands[i] < _bandBuffer[i])
            {
                _bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void CreateAudioBand()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBands[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBands[i];
            }
            _audioBand[i] = (_freqBands[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = audioProfile;
        }
    }

    private void GetAmplitude()
    {
        float _currenAmplitude = 0;
        float _currentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _currenAmplitude += _audioBand[i];
            _currentAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (_currenAmplitude > _amplitudeHighest)
        {
            _amplitudeHighest = _currenAmplitude;
        }

        _amplitude = _currenAmplitude / _amplitudeHighest;
        _amplitudeBuffer = _currentAmplitudeBuffer / _amplitudeHighest;
    }
}
