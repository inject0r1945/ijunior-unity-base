using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }

    public void Move(Vector3 position)
    {
        _aiPath.canSearch = true;
        _aiPath.destination = position;
    }

    public void Stop()
    {
        _aiPath.canSearch = false;
        _aiPath.SetPath(null);
    }
}
