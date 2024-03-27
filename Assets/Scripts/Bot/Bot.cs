using UnityEngine;

[RequireComponent(typeof(BotCollectsResource))]
[RequireComponent(typeof(BotBuildsBase))]
public class Bot : MonoBehaviour
{
    [SerializeField] private bool _isAvailable;

    private BotCollectsResource _collectsResource;
    private BotBuildsBase _buildBase;

    public bool IsAvailable => _isAvailable;
    public BotCollectsResource CollectsResource => _collectsResource;
    public BotBuildsBase BuildBase => _buildBase;

    private void Awake()
    {
        _collectsResource = GetComponent<BotCollectsResource>();
        _buildBase = GetComponent<BotBuildsBase>();
    }

    public void Borrow() => _isAvailable = false;

    public void Release() => _isAvailable = true;

}
