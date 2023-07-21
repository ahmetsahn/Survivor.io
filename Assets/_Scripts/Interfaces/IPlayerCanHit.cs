using UnityEngine;

public interface IPlayerCanHit
{
    public GameObject getShotEffect { get; set; }
    void Hit();
}
