using System.Collections;
using UnityEngine;

public class BotBuildsBase : BotMover
{
    [SerializeField] private Base _basePrefab;

    private Transform _newBasePostion = null;

    public IEnumerator BeginBuildBase(Base currentBase)
    {
        if(CounterOre)
        Bot.Borrow();
        Vector3 finishPosition = new Vector3(_newBasePostion.position.x, transform.position.y, _newBasePostion.position.z);

        yield return MoveTo(finishPosition);

        ChangeStartPosition(transform.position);
        Base newBase = Instantiate(_basePrefab, _newBasePostion.position, Quaternion.identity);
        currentBase.RemoveFlagAction?.Invoke();
        currentBase.RemoveBaseConstructionPriority();
        ChangeCollectionPosition(newBase.CollectPoint);
        Bot.Release();
    }

    public void GetPositionNewBase(Transform newBasePosition) => _newBasePostion = newBasePosition;
}
