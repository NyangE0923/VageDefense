using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    AudioSource[] bgmPlayer;
    int BGMchannelIndex;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum sfx {CornAttack, BeanAttack, EnemyAttack, CarrotAttack, BossAttack, UI, TowerCreate,
    TowerUpgrade, TowerDisable, EnemyDamage, CherryTomatoBoom}

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        Init();
    }
    
    void Init()
    {
        BGMchannelIndex = bgmClip.Length;

        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = new AudioSource[BGMchannelIndex];

        for(int index = 0; index < bgmPlayer.Length; index++)
        {
            bgmPlayer[index] = bgmObject.AddComponent<AudioSource>();
            bgmPlayer[index].playOnAwake = false;
            bgmPlayer[index].loop = true;
            bgmPlayer[index].volume = bgmVolume;
            bgmPlayer[index].clip = bgmClip[index];
        }

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay, int index)
    {
        if (isPlay)
        {
            bgmPlayer[index].Play();
        }
        else
        {
            bgmPlayer[index].Stop();
        }
    }
    public void PlaySfx(sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length;index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
