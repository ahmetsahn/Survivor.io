using UnityEngine;

public class PlayerKeyboardInput : MonoBehaviour, IInput
{
    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }

    public void ReadInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }


    private void Update()
    {
        if (GameManager.Instance.state == GameStates.Pause) return;
        ReadInput();
    }

}
