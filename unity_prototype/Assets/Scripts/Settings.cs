using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider Slider_music;
    public Slider Slider_sound;
    public List<AudioSource> sound_background;
    public AudioSource sound_clickbutton;
   
    public void onEnablebackground()
    {
        Slider_music.onValueChanged.AddListener(delegate { changeVolumebackground(Slider_music.value); });
    }
    public void changeVolumebackground(float sliderValue)
    {
        for (int i = 0; i < sound_background.Count; i++)
        {
            sound_background[i].volume = sliderValue;
        }
        
    }
    public void onEnableclickbutton()
    {
        Slider_sound.onValueChanged.AddListener(delegate { changeVolumeclickbutton(Slider_sound.value); });
    }
    public void changeVolumeclickbutton(float sliderValue)
    {
        sound_clickbutton.volume = sliderValue;
    }
}
