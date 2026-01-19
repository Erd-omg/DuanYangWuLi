using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public GameObject canvas;
    public VideoPlayer videoPlayer;
    public Button exitBtn;
    // 在类中添加 public 引用
    public GameObject specificCharacter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& other.gameObject == specificCharacter) 
        {
            canvas.SetActive(true);
            videoPlayer.Play(); // 播放视频
            exitBtn.gameObject.SetActive(false);
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        // 视频播放完成后显示按钮
        exitBtn.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        // 点击按钮退出游戏
        Application.Quit();
    }
}