using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MV", Mathf.Log10 (sliderValue) * 20);
    }
}
