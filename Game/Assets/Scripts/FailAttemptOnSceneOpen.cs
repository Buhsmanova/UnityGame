using UnityEngine;

/// <summary>
/// Вешается на объект в сцене Fail. При старте увеличивает счетчик неудач.
/// </summary>
public class FailAttemptOnSceneOpen : MonoBehaviour
{
    private void Start()
    {
        FailAttemptsStorage.AddFail();
    }
}
