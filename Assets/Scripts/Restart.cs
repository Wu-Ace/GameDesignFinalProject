using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // 此方法绑定到按钮的OnClick事件
    public void ReloadScene()
    {
        // 获取当前场景的索引，并重新加载它
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void ExitGame()
    {
        // 退出游戏
        Application.Quit();
    }
}