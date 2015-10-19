using UnityEngine;
using System.Collections;

public class PNJScript : MonoBehaviour
{

    private string _message;
    private int _etat;
    private bool _textActive;
    private Animator _animator;
    private Player _player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _textActive = true;
            BottomInfoText.ShowText(_message);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _textActive = false;
            BottomInfoText.HideText();
        }
    }

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _animator=GetComponent<Animator>();
        _message = "Question0\n 1-Reponse01\n2-Reponse02\n3-Reponse03";
        _etat = 0;
        _textActive = false;
    }

    void Update()
    {
        if (!_textActive)
            return;
        switch (_etat)
        {
            case 0: if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _message = "Question1\n 1-Reponse11\n2-Reponse12\n2-Reponse13";
                    BottomInfoText.ShowText(_message);
                    _etat = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _message = "Question2\n 1-Reponse21\n2-Reponse22";
                    BottomInfoText.ShowText(_message);
                    _etat = 2;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _etat = 0;
                    _message = "Question0\n 1-Reponse01\n2-Reponse02\n3-Reponse03";
                    BottomInfoText.ShowText(_message);
                    _textActive = false;
                    BottomInfoText.HideText();
                    _animator.SetTrigger("attack");
                   
                }
                break;
            case 1: if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _message = "Question1\n 1-Reponse21\n2-Reponse22";
                    BottomInfoText.ShowText(_message);
                    _etat = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _etat = 0;
                    _message = "Question0\n 1-Reponse01\n2-Reponse02\n3-Reponse03";
                    BottomInfoText.ShowText(_message);
                    _textActive = false;
                    BottomInfoText.HideText();
                    _animator.SetTrigger("attack");
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _etat = 0;
                    _message = "Question0\n 1-Reponse01\n2-Reponse02\n3-Reponse03";
                    BottomInfoText.ShowText(_message);
                    _textActive = false;
                    BottomInfoText.HideText();
                    _animator.SetTrigger("attack");

                }
                break;
            case 2: if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Application.LoadLevel("level3");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _etat = 0;
                    _message = "Question0\n 1-Reponse01\n2-Reponse02\n3-Reponse03";
                    BottomInfoText.ShowText(_message);
                    _textActive = false;
                    BottomInfoText.HideText();
                    _animator.SetTrigger("attack");
                }
                
                break;
                

        }
    }

    public void KillPlayer()
    {
        _player.GetComponent<CharacterController2D>().HandleCollisions = false;
        _player.collider2D.enabled = false;
        LevelManager.Instance.KillPlayer();
    }
}
