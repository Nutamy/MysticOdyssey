using UnityEngine.SceneManagement;

public static class SceneTransition 
{
    public static void Initiate(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
