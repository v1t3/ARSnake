using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadNext()
    {
        SceneManager.LoadScene("Game/Scenes/Game");
    }
}
