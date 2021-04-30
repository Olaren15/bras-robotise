using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void Restart()
    {
        Physics.gravity = new Vector3(0.0f, -9.81f, 0.0f);
        SceneManager.LoadScene(0);
    }
}