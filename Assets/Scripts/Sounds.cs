using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource slapSource;
    public AudioClip slapClip;
    public AudioSource clickSource;
    public AudioClip clickClip;
    public AudioSource snapSource;
    public AudioClip snapClip;
    public AudioSource breakSource;
    public AudioClip breakClip;
    public AudioSource thumpSource;
    public AudioClip thumpClip;
    public AudioSource tetrisSource;
    public AudioClip tetrisClip;
    public AudioSource tspinSource;
    public AudioClip tspinClip;
    public AudioSource comboBreakSource;
    public AudioClip comboBreakClip;
    public AudioSource stunSource;
    public AudioClip stunClip;
    public AudioSource[] lineClearSources;
    public AudioClip[] lineClearClips;
    public void PlaySlap()
    {
        slapSource.PlayOneShot(slapClip);
    }
    public void PlayClick()
    {
        clickSource.PlayOneShot(clickClip);
    }
    public void PlaySnap()
    {
        snapSource.PlayOneShot(snapClip);
    }
    public void PlayBreak()
    {
        breakSource.PlayOneShot(breakClip);
    }
    public void PlayThump()
    {
        thumpSource.PlayOneShot(thumpClip);
    }
    public void PlayLineClear(int combo)
    {
        lineClearSources[combo].PlayOneShot(lineClearClips[combo]);
    }
    public void PlayComboBreak()
    {
        comboBreakSource.PlayOneShot(comboBreakClip);
    }
    public void PlayTetris()
    {
        tetrisSource.PlayOneShot(tetrisClip);
    }
    public void PlayTspin()
    {
        tspinSource.PlayOneShot(tspinClip);
    }
    public void PlayStun()
    {
        stunSource.PlayOneShot(stunClip);
    }
}