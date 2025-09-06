using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource sfxAudio;

    [Header("BGM Sound")]
    [SerializeField] AudioClip bgmMain;
    [SerializeField] AudioClip mainMenu;
    [SerializeField] AudioClip settingPause;

    [Space]

    [Header("SFX Sound")]
    [SerializeField] AudioClip pelangganPesen;
    [SerializeField] AudioClip pelangganMasuk;
    [SerializeField] AudioClip pelangganHappy;
    [SerializeField] AudioClip pelangganMarah;
    [SerializeField] AudioClip yesHigh;
    [SerializeField] AudioClip notHigh;
    [SerializeField] AudioClip riceCooker;


    void Start()
    {

    }

    //bgm

    public void playMainBGM()
    {
        audio.clip = bgmMain;
        audio.loop = true;
        audio.Play();
    }

    public void playPauseSettingBGM()
    {
        audio.clip = settingPause;
        audio.loop = true;
        audio.Play();
    }

    public void playHomeBGM()
    {
        audio.clip = mainMenu;
        audio.loop = true;
        audio.Play();
    }

    //sfx

    // SFX
    public void PlayPelangganPesen()
    {
        audio.PlayOneShot(pelangganPesen);
    }

    public void PlayPelangganMasuk()
    {
        audio.PlayOneShot(pelangganMasuk);
    }

    public void PlayPelangganHappy()
    {
        audio.PlayOneShot(pelangganHappy);
    }

    public void PlayPelangganMarah()
    {
        audio.PlayOneShot(pelangganMarah);
    }

    public void PlayYesHigh()
    {
        audio.PlayOneShot(yesHigh);
    }

    public void PlayNotHigh()
    {
        audio.PlayOneShot(notHigh);
    }

    public void PlayRiceSound()
    {
        sfxAudio.clip = riceCooker;
        sfxAudio.Play();
        StartCoroutine(laguBerhenti(0.5f));
    }

    IEnumerator laguBerhenti(float detik)
    {
        yield return new WaitForSeconds(detik);
        sfxAudio.Stop();
    }

}
