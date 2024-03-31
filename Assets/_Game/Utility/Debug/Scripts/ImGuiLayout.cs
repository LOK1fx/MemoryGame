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
        private LocalisationSystem.ELanguage _currentLanguage;


        private void OnEnable()
        {
            ImGuiUn.Layout += OnLayout;

            _currentPlayer = FindObjectOfType<Player.Player>();
            _currentLanguage = LocalisationSystem.Language;
        }
        
        private void OnDisable()
        {
            ImGuiUn.Layout -= OnLayout;

            _currentPlayer = null;
        }

        private void OnLayout()
        {
            //ImGui.ShowDemoWindow();

            if (Input.GetKey(KeyCode.Mouse4)) // for screenshots and etc.
                return;


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

            ImGui.Begin("Dev debug window (Press F3 for cursor)", ref _isDevMenuOpened);

            ImGui.Text("General");
            ImGui.Separator();

            ImGui.Text($"Game time (unscaled): {Time.unscaledTime}");
            ImGui.Text($"Game FPS (unscaled): {(int)(1f / Time.unscaledDeltaTime)}");
            ImGui.Text($"Game locale (hashed): {_currentLanguage}");

            ImGui.Separator();

            ImGui.Spacing();

            DrawPlayerInfo();
            DrawGameManagerMenu();
            DrawCameraMenu();

            if (_currentPlayer != null)
                DrawPlayerMenu();

            DrawCutsceneMenu();
            DrawLevelManagerMenu();
            DrawMainMenuDebugger();

            ImGui.End();
        }

        private void DrawPlayerInfo()
        {
            if (_currentPlayer != null)
            {
                ImGui.Text($"Player: {_currentPlayer.name}");

                if (Controller.TryGetController<PlayerController>(out var playerController) == true)
                    ImGui.Text($"Controller: {playerController.name}");

                ImGui.Separator();

                ImGui.Text($"Player velocity: {_currentPlayer.Movement.Rigidbody.velocity}");
                ImGui.Text($"Player position: {_currentPlayer.transform.position}");
                ImGui.Text($"Player Islocal: {_currentPlayer.IsLocal}");
                ImGui.Text($"Player IsDead: {_currentPlayer.IsDead}");
                ImGui.Text($"Player InTransport: {_currentPlayer.State.InTransport}");

                ImGui.Separator();
            }
        }

        private void DrawGameManagerMenu()
        {
            if (ImGui.CollapsingHeader("Game manager"))
            {
                ImGui.Text("Locale");
                ImGui.Separator();

                ImGui.BeginGroup();

                if (ImGui.Button("Set English locale", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    LocalisationSystem.Language = LocalisationSystem.ELanguage.English;
                }
                if (ImGui.Button("Set Russian locale", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    LocalisationSystem.Language = LocalisationSystem.ELanguage.Russian;
                }

                ImGui.EndGroup();

                if (ImGui.Button("Reload LocalisationSystem", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    LocalisationSystem.Init();

                    _currentLanguage = LocalisationSystem.Language;
                }


                ImGui.Text("Time");
                ImGui.Separator();

                if (ImGui.Button("Set timescale to 5x", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    Time.timeScale = 5f;
                }
                if (ImGui.Button("Set timescale to 3x", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    Time.timeScale = 3f;
                }
                if (ImGui.Button("Set timescale to 1x", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    Time.timeScale = 1f;
                }
            }
        }

        private void DrawCameraMenu()
        {
            if (ImGui.CollapsingHeader("Camera manager"))
            {
                if (ImGui.Button("Activate freecam", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    if (_isFreecamActive)
                    {
                        Destroy(_currentFreecam);

                        if (_currentPlayer != null)
                        {
                            _currentPlayer.Movement.StartMovementInput();
                            _currentPlayer.Camera.StartInput();
                            _currentPlayer.ItemManager.StartInput(this);
                        }

                        _isFreecamActive = false;
                    }
                    else
                    {
                        if (_currentPlayer != null)
                        {
                            _currentPlayer.Movement.StopMovementInput();
                            _currentPlayer.Camera.StopInput();
                            _currentPlayer.ItemManager.StopInput(this);
                        }

                        var camera = Camera.main;

                        if (_currentFreecam == null)
                            _currentFreecam = Instantiate(_freecamPrefab, camera.transform.position, camera.transform.rotation);

                        _isFreecamActive = true;
                    }
                }

                if (_currentFreecam != null)
                {
                    if(_currentPlayer != null)
                    {
                        if (ImGui.Button("Teleport player to freecam", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                        {
                            _currentPlayer.transform.position = _currentFreecam.transform.position;
                        }
                    }
                    
                    if (ImGui.Button("[V] Switch light mode", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                    {
                        _currentFreecam.GetComponent<DebugFreecam>().SwitchLightMode();
                    }
                    if (ImGui.Button("[C] Switch debug view", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                    {
                        _currentFreecam.GetComponent<DebugFreecam>().SwitchDebugViewMode();
                    }
                }
            }
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

        private void DrawLevelManagerMenu()
        {
            if (ImGui.CollapsingHeader("Level manager"))
            {
                if (ImGui.Button("Reload current level", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
                }


                ImGui.Separator();

                if (ImGui.Button("Load MainMenu", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
                }
                if (ImGui.Button("Load WakeUp", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync("WakeUp_Core", LoadSceneMode.Single);
                }
                if (ImGui.Button("Load RoomButtons", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync("RoomButtons", LoadSceneMode.Single);
                }
                if (ImGui.Button("Load EntranceToMine", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync("EntranceToMine", LoadSceneMode.Single);
                }
                if (ImGui.Button("Load Labirint01", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync("Labirint01_Core", LoadSceneMode.Single);
                }
                if (ImGui.Button("Load Shore", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                {
                    SceneManager.LoadSceneAsync("Shore_Core", LoadSceneMode.Single);
                }
            }
        }

        private void DrawMainMenuDebugger()
        {
            if (ImGui.CollapsingHeader("MainMenu background"))
            {
                if (MenuBackgroundRemember.Instance != null)
                {
                    if (SceneManager.GetActiveScene().name == "MainMenu")
                    {
                        if (ImGui.Button("Set WakeUp", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                        {
                            MenuBackgroundRemember.Instance.Remember(ELevelName.WakeUp_01);

                            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
                        }
                        if (ImGui.Button("Set RoomButtons", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                        {
                            MenuBackgroundRemember.Instance.Remember(ELevelName.RoomsButton);

                            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
                        }
                        if (ImGui.Button("Set EntranceToMine", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                        {
                            MenuBackgroundRemember.Instance.Remember(ELevelName.EntranceToMine);

                            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
                        }
                        if (ImGui.Button("Set Labirint01", new Vector2(BUTTON_SIZE_X, BUTTON_SIZE_Y)))
                        {
                            MenuBackgroundRemember.Instance.Remember(ELevelName.Labirint01_03);

                            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
                        }
                    }
                    else
                    {
                        ImGui.Text("You arn't in MainMenu right now");
                    }
                }
                else
                {
                    ImGui.Text("There are no MenuBackgroundRemember Instance");
                }
            }
        }
    }
}
