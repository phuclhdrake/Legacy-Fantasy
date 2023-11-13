using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController intance ; 
    public Slider _musicSlider, _sfxSlider;
    [SerializeField] public Text txtKill ; 
    public Image imgMusic, imgSFX;
    private bool isMusic, isSFX;

    // Start is called before the first frame update
    private void Awake() {
        intance = this ; 
    }
    private void Start() {
        if( txtKill != null)
        {
            txtKill.text = PlayerController.Instance.numberKill.ToString();
        }
    }
    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
        isMusic = !isMusic;
        if (isMusic)
        {
            imgMusic.color = Color.gray;
        }
        else
        {
            imgMusic.color = Color.white;
        }
    }
    public void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();
        isSFX = !isSFX;
        if (isSFX)
        {
            imgSFX.color = Color.gray;
        }
        else
        {
            imgSFX.color = Color.white;
        }
    }
    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(_sfxSlider.value);
    }
    

}
