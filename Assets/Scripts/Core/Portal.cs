using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constants.PLAYER_TAG)) return;
        print("Player detected!");
        SceneTransition.Initiate(nextSceneIndex);
    }
}
