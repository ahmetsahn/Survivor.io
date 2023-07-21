using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    [field : SerializeField] public Animator TopTorso { get; set; }
    [field : SerializeField] public Animator BotTorso { get; set; }

    
    public void PlayDeathAnimation()
    {
        Destroy(gameObject);
    }

    public void HandleDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

}
