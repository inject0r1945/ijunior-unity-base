using UnityEngine;

namespace RTS.Resources
{
    [CreateAssetMenu(fileName = "ResourceInfo", menuName = "ResourceInfo")]
    public class ResourceInfo : ScriptableObject
    {
        [SerializeField] private Resource _prefab;
        [SerializeField] private Sprite _sprite;

        public Resource Prefab => _prefab;

        public Sprite Sprite => _sprite;

        public Resource Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            Resource resource = Instantiate(_prefab, position, rotation, parent);
            return resource;
        }
    }
}
