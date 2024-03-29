using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImGuiNET;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

namespace LOK1game.DebugTools
{
    public class ImGuiLayout : MonoBehaviour
    {
        private const float BUTTON_SIZE_X = -1f;
        private const float BUTTON_SIZE_Y = 20f;


        private bool _isDevMenuOpened;
        private bool _isFreecamActive;


        [SerializeField] private GameObject _freecamPrefab;

        private GameObject _currentFreecam;
        private Player.Player _currentPlayer;

        private void OnEnable()
        {
            ImGuiUn.Layout += OnLayout;

            _currentPlayer = FindObjectOfType<Player.Player>();
        }
        
        private void OnDisable()
        {
            ImGuiUn.Layout -= OnLayout;

            _currentPlayer = null;
        }

        private void OnLayout()
        {
            //ImGui.ShowDemoWindow();

            if(Input.GetKeyDown(KeyCode.F3))
            {
                if(Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            ImGui.Begin("Dev window", ref _isDevMenuOpened);

            DrawPlayerMenu();
            DrawCutsceneMenu();

            ImGui.End();
        }

        private void DrawPlayerMenu()
        {
            if (ImGui.CollapsingHeader("Player"))
            {
                if (ImGui.Button("Teleport player to exit (only in Labirint01)", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    if (SceneManager.GetSceneByName("Labirint01_01").isLoaded == true)
                        SceneManager.UnloadScene("Labirint01_01");

                    if (SceneManager.GetSceneByName("Labirint01_03").isLoaded == false)
                        SceneManager.LoadScene("Labirint01_03", LoadSceneMode.Additive);

                    _currentPlayer.transform.position = new Vector3(93.75f, 3.47f, 49.56f);
                }

                if (ImGui.Button("Activate freecam", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    if (_isFreecamActive)
                    {
                        Destroy(_currentFreecam);

                        if (_currentPlayer != null)
                        {
                            _currentPlayer.Movement.StartMovementInput();
                            _currentPlayer.Camera.SetDefaultViewAngles();
                            _currentPlayer.ItemManager.StartInput(this);
                        }

                        _isFreecamActive = false;

                        return;
                    }
                    else
                    {
                        if (_currentPlayer != null)
                        {
                            _currentPlayer.Movement.StopMovementInput();
                            _currentPlayer.Camera.LimitViewAngles(0f, 0f);
                            _currentPlayer.ItemManager.StopInput(this);
                        }

                        _isFreecamActive = true;
                    }

                    var camera = Camera.main;

                    if (_currentFreecam == null)
                        _currentFreecam = Instantiate(_freecamPrefab, camera.transform.position, camera.transform.rotation);
                }
            }
        }

        private void DrawCutsceneMenu()
        {
            if (ImGui.CollapsingHeader("Cutscene manager"))
            {
                if (ImGui.Button("Skip cutscene (if any)", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    var director = FindObjectOfType<PlayableDirector>();

                    if (director != null)
                    {
                        if (director.state == PlayState.Playing)
                            director.time = director.duration - 0.1f;
                    }  
                }
            }
        }
    }
}
