using UnityEngine;

[RequireComponent(typeof(Base))]
public class FlagCreator : MonoBehaviour
{
    [SerializeField] private Flag _flag;
    [SerializeField] private bool _canPlacingFlag = false;

    private Flag _currentFlag;
    private Base _base;

    private void Awake() => _base = GetComponent<Base>();

    private void OnEnable() => _base.RemoveFlagAction += RemoveFlag;

    private void OnDisable() => _base.RemoveFlagAction -= RemoveFlag;

    public void SetFlag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Base _))
            {
                _canPlacingFlag = true;
            }
            else if (_canPlacingFlag)
            {
                if (_currentFlag == null)
                    _currentFlag = Instantiate(_flag, hit.point, Quaternion.identity);
                else
                    _currentFlag.transform.position = hit.point;

                _base.GetPostionNewBase(_currentFlag.transform);
                _base.SetBaseConstructionPriority();
                _canPlacingFlag = false;
            }
        }
    }

    public void RemoveFlag() => Destroy(_currentFlag.gameObject);
}
