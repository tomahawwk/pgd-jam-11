using UnityEngine;

namespace Prototype.Logic.Interactions
{
    public class RemoveSceneObject : MonoBehaviour
    {
        public void Remove()
        {
            SceneRemover.RemoveCurrentScene();
        }
    }
}