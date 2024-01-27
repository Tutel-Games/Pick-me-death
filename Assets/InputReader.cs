using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool W;
    public bool A;
    public bool D;
    public bool S;
    void Update()
    {
        W = Input.GetKeyDown(KeyCode.W);
        A = Input.GetKeyDown(KeyCode.A);
        D = Input.GetKeyDown(KeyCode.D);
        S = Input.GetKeyDown(KeyCode.S);
    }
}
