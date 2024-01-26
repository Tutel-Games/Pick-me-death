using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool IsJumping;
    public bool A;
    public bool D;
    void Update()
    {
        IsJumping =  Input.GetKeyDown(KeyCode.Space);
        A = Input.GetKeyDown(KeyCode.A);
        D = Input.GetKeyDown(KeyCode.D);
    }
}
