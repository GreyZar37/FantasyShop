using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    
    public List<AudioClip> sellSound = new List<AudioClip>();
    public List<AudioClip> collectMoneySound = new List<AudioClip>();

    public List<AudioClip> itemPlacementSound = new List<AudioClip>();

    public List<AudioClip> stepsSoundStone = new List<AudioClip>();
    public List<AudioClip> stepsSoundDirt = new List<AudioClip>();


    public AudioSource SFX, music, ambience;

    public void playSellSound()
    {
        int random = Random.Range(0, sellSound.Count);
        SFX.PlayOneShot(sellSound[random]);
    }

    public void playCollectMoneySound()
    {
        int random = Random.Range(0, collectMoneySound.Count);
        SFX.PlayOneShot(collectMoneySound[random]);
    }
    public void playItemPlacementSound()
    {
        int random = Random.Range(0, itemPlacementSound.Count);
        SFX.PlayOneShot(itemPlacementSound[random]);
    }

    public void playStepsSoundStone()
    {
        int random = Random.Range(0, stepsSoundStone.Count);
        SFX.PlayOneShot(stepsSoundStone[random]);
    }
    public void playStepsSoundDirt()
    {
        int random = Random.Range(0, stepsSoundDirt.Count);
        SFX.PlayOneShot(stepsSoundDirt[random]);
    }

}
