using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource click;
    // Start is called before the first frame update
    public void playButton()
    {
        click.Play();
    }
}
