using UnityEngine;
using System.Collections;
// ’Ç‰Á
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
        OnPanel.SetActive(true);        // PanelMenu¤òtrue¤Ë¤¹¤ë
        OnUnPanel.SetActive(false);     // PanelEsc¤òfalse¤Ë¤¹¤ë
        Time.timeScale = 0;
        pauseGame = true;
  //      FirstPersonController fpc = player.GetComponent<FirstPersonController>();
    //    fpc.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;     // •W€ƒ‚[ƒh
        Cursor.visible = true;    // ƒJ[ƒ\ƒ‹•\¦
    }

    public void OnUnPause()
    {
        OnPanel.SetActive(false);       // PanelMenu‚ğfalse‚É‚·‚é
        OnUnPanel.SetActive(true);      // PanelEsc‚ğtrue‚É‚·‚é
        Time.timeScale = 1;
        pauseGame = false;
      //  FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        //fpc.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;   // ’†‰›‚ÉƒƒbƒN
        Cursor.visible = false;     // ƒJ[ƒ\ƒ‹”ñ•\¦
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