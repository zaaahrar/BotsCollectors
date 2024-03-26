using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const int NubmerLeftKeyMouse = 0;

    [SerializeField] private FlagCreator _flagCreator;

    private void Update()
    {
        if (Input.GetMouseButtonDown(NubmerLeftKeyMouse))
            _flagCreator.SetFlag();
    }
}
