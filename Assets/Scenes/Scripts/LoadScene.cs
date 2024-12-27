using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByIndex(int index) => SceneManager.LoadScene(index);

}
