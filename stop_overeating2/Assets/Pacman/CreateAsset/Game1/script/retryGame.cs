using UnityEngine;
using System.Collections;
// �ǉ�
using UnityEngine.SceneManagement;
//using UnityStandardAssets.Characters.FirstPerson;

public class retryGame : MonoBehaviour {

    public GameObject player;
    public GameObject OnPanel, OnUnPanel;
    private bool pauseGame = false;
    
    void Start()
    {
        OnUnPause();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame == true)
            {
                OnPause();
            }
            else
            {
                OnUnPause();
            }
        }    
    }

    public void OnPause()
    {
        OnPanel.SetActive(true);        // PanelMenu��true�ˤ���
        OnUnPanel.SetActive(false);     // PanelEsc��false�ˤ���
        Time.timeScale = 0;
        pauseGame = true;
  //      FirstPersonController fpc = player.GetComponent<FirstPersonController>();
    //    fpc.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;     // �W�����[�h
        Cursor.visible = true;    // �J�[�\���\��
    }

    public void OnUnPause()
    {
        OnPanel.SetActive(false);       // PanelMenu��false�ɂ���
        OnUnPanel.SetActive(true);      // PanelEsc��true�ɂ���
        Time.timeScale = 1;
        pauseGame = false;
      //  FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        //fpc.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;   // �����Ƀ��b�N
        Cursor.visible = false;     // �J�[�\����\��
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Scene_01");
    }
    
    public void OnResume()
    {
        OnUnPause();
    }
}