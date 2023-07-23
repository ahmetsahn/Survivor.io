using UnityEngine;

public class PlayerAnimation
{

    private readonly Animator topTorso;
    private readonly Animator botTorso;


    public PlayerAnimation(Animator topTorso,Animator botTorso)
    {
        this.topTorso = topTorso;
        this.botTorso = botTorso;
    }

    public void PlayShootAnimation()
    {
        topTorso.Play("Shoot");
    }

    public void SetTopTorsoRotation(Quaternion newRotation)
    {
        topTorso.transform.rotation = newRotation;
    }

    public void SetBotTorsoRotation(Quaternion newRotation)
    {
        botTorso.transform.rotation = newRotation;
    }

    public float GetBotTorsoLocalEulerAnglesZ()
    {
        return botTorso.transform.localEulerAngles.z;
    }

    public void SetBotTorsoLocalScale(Vector3 newScale)
    {
        botTorso.transform.localScale = newScale;
    }

    public void BotTorsoMoveAnimation(bool move)
    {
        botTorso.SetBool("IsMoving", move);
    }
}
