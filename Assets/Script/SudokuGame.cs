using System.Collections.Generic;
using System;
using System.Globalization;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SudokuGame : MonoBehaviour
{
    /*   Esse script vai armazenar tudo relacionado à
     *   tela do jogo: Reset, Check, Erase, Pencil, 
     *   Numbers & MakeGame
     */

    public Configurations configurations;
    public JsonSaving jsonSaving;
    public Statistics _statistics;
    public Localization localization;
    public AdsManager adsManager;
    public SFX_Music_Holder audio_holder;
    public GridSquare gridSquare;
    public Sudoku_src sudoku_src;
    
    public CanvasGroup MenuInicial;
    public CanvasGroup TelaJogo;
    public CanvasGroup TelaEstatisticas;
    public CanvasGroup TelaConfig;
    public CanvasGroup JogoInteiro;
    public CanvasGroup TelaVitoria;
    public CanvasGroup TelaTutorial;
    public CanvasGroup TelaSobreNos;

    public AudioSource audioSourceSFX;
    public AudioSource audioSourceMusic;
    public OnOffToggle musicToggle;
    public OnOffToggle SFXToggle;
    public OnOffToggle TimerToggle;
    public Button NoAdsButton;

    public GameObject[] squares;
    public GameObject[,] AxA_squares = new GameObject[9, 9];
    public TextMeshProUGUI selectedSquare;
    public TextMeshProUGUI DificultyText;

    private IEnumerator musicCorroutine;
    public GameObject ContinueBtn;
    public GameObject wrongSolution;
    public Image frontground;
    public int LastScreen = 0;
    
    public bool isPencilActivated = false;
    
    public bool canplaymusic = true;
    public bool canplaySFX = true;
    
    public bool isplaying = false;
    public bool DidYouDeactivateTimeInGame;

    public string solution_string;
    public int[,] Solution;
    public int[,] Game;
    public int[,] gamefill;
    int[,] gameGrid = new int[9, 9];
    
    public int lang = 0;
    public int _difficulty;
<<<<<<< HEAD:PeacefulNumbers/Assets/Script/SudokuGame.cs

    public int S_tempo;

    public bool PlayerPurchasedNoAds = false;

    private void Awake()
=======

    public int S_tempo;


    private void Start()
>>>>>>> parent of 54cfe34e (Ver0.9):My project/Assets/Script/SudokuGame.cs
    {
        jsonSaving.SetPaths();
        jsonSaving.GameLoad();
        GameSave gameSave = jsonSaving.gameSave;
        if (jsonSaving.gameSave == null || jsonSaving.gameSave.gamePlayed == "")
        {
            ContinueBtn.SetActive(false);
        }
        
    }

    private void Start()
    {
        musicCorroutine = PlayMusic();
        StartCoroutine(PlayMusic());
        adsManager.LoadAds();
    }


    private void Initiate()
    {
        // Create an Array of Arrays with the Squares GameObjects
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                AxA_squares[i, j] = squares[count];
                count++;
            }
        }
        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && isplaying)
        {
            SaveGame(true);
        }
    }

    #region CREATE AND SET GAME

    public void CreateGame(int difficulty)
    {
        StartCoroutine(CreateNewGame(difficulty));
    }
    
    IEnumerator CreateNewGame(int difficulty)
    {
        Initiate();
        _difficulty = difficulty;
        EnableButtons(false);
        int[,] grid = { { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        configurations.ResetTimer();
        frontground.gameObject.SetActive(false);
        sudoku_src.GenerateSolvedSudoku(grid);
        Solution = (int[,])grid.Clone();
        solution_string = sudoku_src.CreateString(Solution);
        gameGrid = sudoku_src.CreateGameByDificulty(grid, _difficulty);
        Game = (int[,])gameGrid.Clone();
        isplaying = true;
        _statistics.LoadStatistics();
        _statistics.IncreaseGameStatistics(_difficulty, 1, 0, 0);
        _statistics.SaveStatistics();
        SetGameOnSquares();
        sudoku_src.FadePanel(false, MenuInicial);
        SetDifText(_difficulty, lang);
        selectedSquare = null;
        yield return null;
    }

    public void SetGameOnSquares()
    {
        sudoku_src.FadePanel(true, TelaJogo);
        sudoku_src.ResetSquare(AxA_squares);
        sudoku_src.ResetFont(AxA_squares);
        sudoku_src.EraseAllPencil(AxA_squares);

        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GridSquare text_square = AxA_squares[i, j].GetComponent<GridSquare>();
                text_square.DisplayText(Game[i, j]);
                count++;
            }
        }

        EnableButtons(true);
        configurations.StartTimer();
    }

    IEnumerator SetFillOnSquares()
    {
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (gamefill[i,j] != 0)
                {
                    GridSquare text_square = AxA_squares[i, j].GetComponent<GridSquare>();
                    text_square.number_text.text = gamefill[i, j].ToString();
                    text_square.number_text.color = text_square.AquaGreen;
                }
                count++;
            }
        }
        yield return null;
    }
    // END OF CREATE AND SET GAME

    public string FindArrayPos(string name)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (AxA_squares[i, j].name == name)
                {
                    string pos = $"{i}{j}";
                    return pos;
                }
            }
        }
        return null;
    }

    private void EnableButtons(bool interactable)
    {
        foreach (var item in AxA_squares)
        {
            item.GetComponent<Button>().interactable = interactable;
        }
    }

    public void ResetGame()
    {
        configurations.StopTimer();
        configurations.ResetTimer();
        selectedSquare = null;
        SetGameOnSquares();
    }

    #endregion

    #region Solution Checker
    public void CheckSolution()
    {
        // Criar a string para comparar se a solução dada pelo jogador é a mesma solução do sudoku
        var GivenSolution = new System.Text.StringBuilder();
        foreach (var item in AxA_squares)
        {
            GridSquare grid = item.GetComponent<GridSquare>();
            if (grid.number_text.text == " ")
            {
                GivenSolution.Append("0");
            }
            else
            {
                GivenSolution.Append(grid.number_text.text);
            }
        }

        // Verifique se as solução são iguais
        if (sudoku_src.CreateString(Solution) == GivenSolution.ToString())
        {
            Debug.Log ("Ganhou!");
            adsManager.ShowWinAd();
            return;
        }

        // Caso não forem, verificar se tem zero na solução do jogador (ou seja, se tem algo que não foi preenchido)
        // Se tiver, retorne
        for (int i = 0; i < GivenSolution.Length; i++)
        {
            if (GivenSolution[i] == '0')
            {
                return;
            }
        }

        // Se não tiver zeros, abrir tela de "solução errada"
        SolutionIsWrong();


    }

    public void SolutionIsWrong()
    {
        configurations.StopTimer();
        frontground.gameObject.SetActive(true);
        wrongSolution.SetActive(true);
    }


    public void Win()
    {
        configurations.StopTimer();

        if (DidYouDeactivateTimeInGame)
        {
            _statistics.IncreaseGameStatistics(_difficulty, 0, 1, 0);
            _statistics.SaveStatistics();
        }
        else
        {
            _statistics.IncreaseGameStatistics(_difficulty, 0, 1, 0);
            _statistics.CompareTimes(_difficulty, (int)configurations.timeInSeconds);
            _statistics.SaveStatistics();

        }

        frontground.gameObject.SetActive(true);            
        sudoku_src.FadePanel(true, TelaVitoria);
        isplaying = false;
        DidYouDeactivateTimeInGame = false;
        PlaySFX(3);
    }
    #endregion

    #region Buttons

    public void EraseNumber()
    {
        try
        {
            if (gridSquare == null)
            {
                return;
            }

            if (gridSquare != null & gridSquare.CanChangeNubers & selectedSquare != null & selectedSquare.text != " ")
            {
                selectedSquare.text = " ";
                gridSquare.EraseHighlight();
            }
        }
        catch (NullReferenceException)
        {
            return;
        }
    }

    public void SetNumber(int number)
    {
        PencilTexts pencilTexts = gridSquare.GetComponent<PencilTexts>();
        PlaySFX(0);

        if (isPencilActivated & gridSquare != null & gridSquare.CanChangeNubers & selectedSquare != null & gridSquare)
        {
            if (selectedSquare.text != " ")
            {
                selectedSquare.text = " ";
            }

            foreach (var item in pencilTexts.penciltexts)
            {
                TextMeshProUGUI textMeshProUGUI = item.GetComponent<TextMeshProUGUI>();
                if (item.name == $"Pencil_text ({number})" & textMeshProUGUI.enabled == false)
                {
                    textMeshProUGUI.enabled = true;
                }
                else if (item.name == $"Pencil_text ({number})" & textMeshProUGUI.enabled == true)
                {
                    item.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }

        try
        {
            if (gridSquare == null)
            {
                return;
            }

            if (gridSquare != null & gridSquare.CanChangeNubers & selectedSquare != null & gridSquare & selectedSquare.text == " " & !isPencilActivated)
            {
                // Esse daqui pega todos os lapís dentro desse quadrado e desliga.
                foreach (var item in pencilTexts.penciltexts)
                {
                    item.GetComponent<TextMeshProUGUI>().enabled = false;
                }

                // Preciso de um código que pegue todos os quadrados dentro da linha, coluna e grid9x9 e apague o lápis correspondente ao número.
                sudoku_src.ErasePencil(gridSquare.rowsquares, number);
                sudoku_src.ErasePencil(gridSquare.columnquares, number);
                sudoku_src.ErasePencil(gridSquare.grid9x9squares, number);

                // E então seta que o número é aquele.
                gridSquare.gameObject.SetActive(true);
                selectedSquare.text = number.ToString();
                gridSquare.ExternalHighlight();
                
                
            }
        }
        catch (NullReferenceException)
        {
            return;
        }
        CheckSolution();
    }

    public void SetPencil(GameObject gameObject)
    {
        isPencilActivated = !isPencilActivated;
        sudoku_src.PencilColor(gameObject, isPencilActivated);
        PlaySFX(0);
    }
    

    public void VoltarParaMenuPrincipal()
    {
        frontground.gameObject.SetActive(false);
        sudoku_src.FadePanel(false, TelaConfig);
        

        if (LastScreen == 0)
        {
            isplaying = false;
            sudoku_src.FadePanel(true, MenuInicial);
        }
        if(LastScreen == 1)
        {
            isplaying = true;
            configurations.StartTimer();
            sudoku_src.FadePanel(true, TelaJogo);
        }
        
    }

    public void ContinuarDepois()
    {
        isplaying = false;
        SaveGame(true);
        DidYouDeactivateTimeInGame = false;
        S_tempo = (int)configurations.timeInSeconds;
        selectedSquare = null;
        sudoku_src.FadePanel(false, TelaJogo);
        sudoku_src.FadePanel(false, TelaVitoria);
        sudoku_src.FadePanel(true, MenuInicial);
    }

    public void AbandonarJogo()
    {
        DidYouDeactivateTimeInGame = false;
        isplaying = false;
        SaveGame(false);

        _statistics.IncreaseGameStatistics(_difficulty, 0, 0, 1);
        _statistics.SaveStatistics();

        selectedSquare = null;
        sudoku_src.FadePanel(false, TelaJogo);
        sudoku_src.FadePanel(false, TelaVitoria);
        sudoku_src.FadePanel(true, MenuInicial);
    }
    #endregion

    #region Screen to screen methods
    public void MenuPrincipal_Config(int WhereYouComeFrom)
    {
        LastScreen = WhereYouComeFrom;
        sudoku_src.FadePanel(false, MenuInicial);
        sudoku_src.FadePanel(true, TelaConfig);
    }

    public void Jogo_Config(int WhereYouComeFrom)
    {
        LastScreen = WhereYouComeFrom;
        configurations.StopTimer();
        sudoku_src.FadePanel(false, TelaJogo);
        sudoku_src.FadePanel(true, TelaConfig);
    }

    public void EstatisticasConfig()
    {
        sudoku_src.FadePanel(false, MenuInicial);
        sudoku_src.FadePanel(true, TelaEstatisticas);
    }

    public void EstatisticasMenu()
    {
        sudoku_src.FadePanel(true, MenuInicial);
        sudoku_src.FadePanel(false, TelaEstatisticas);
        _statistics.dificultyNumber = 0;
        _statistics.SetStats();
    }

    public void Menu_Tutorial()
    {
        sudoku_src.FadePanel(false, MenuInicial);
        sudoku_src.FadePanel(true, TelaTutorial);
    }

    public void Tutorial_Menu()
    {
        sudoku_src.FadePanel(true, MenuInicial);
        sudoku_src.FadePanel(false, TelaTutorial);
    }

    public void Menu_SobreNos()
    {
        sudoku_src.FadePanel(false, MenuInicial);
        sudoku_src.FadePanel(true, TelaSobreNos);
    }

    public void SobreNos_Menu()
    {
        sudoku_src.FadePanel(true, MenuInicial);
        sudoku_src.FadePanel(false, TelaSobreNos);
    }

    public void FecharTelaVitoria()
    {
        sudoku_src.FadePanel(false, MenuInicial);
        sudoku_src.FadePanel(false, TelaVitoria);
    }

    public void WinToMenu()
    {
        DidYouDeactivateTimeInGame = false;
        isplaying = false;
        SaveGame(false);

        selectedSquare = null;
        sudoku_src.FadePanel(false, TelaJogo);
        sudoku_src.FadePanel(false, TelaVitoria);
        sudoku_src.FadePanel(true, MenuInicial);
    }
    #endregion

    #region SAVE FILES FUNCTIONS
    public void SaveGame(bool save_or_not)
    {
        if (save_or_not)
        {
            ContinueBtn.SetActive(true);
            //                  gameDifficulty, gamePlayed, gameFill, solution, timeinsecs, timeDeactivated
            jsonSaving.GameSave(_difficulty, sudoku_src.CreateString(Game), sudoku_src.GetDifferenceGame(squares, Game), solution_string, configurations.timeInSeconds, DidYouDeactivateTimeInGame);
        }
        else
        {
            ContinueBtn.SetActive(false);
            //                  gameDifficulty, gamePlayed, gameFill, solution, timeinsecs, timeDeactivated
            jsonSaving.GameSave(0 , "", "", "", 0, false);
        }

    }

    public void LoadGame()
    {
        jsonSaving.GameLoad();
        GameSave gameSave = jsonSaving.gameSave;
        if(gameSave.gamePlayed == "" | gameSave == null)
        {
            ContinueBtn.SetActive(false);
            return;
        }
        else
        {
            ContinueBtn.SetActive(true);
            if (gameSave != null)
            {
                Game = sudoku_src.CreateMatriz(gameSave.gamePlayed);
                gamefill = sudoku_src.CreateMatriz(gameSave.gameFill);
                S_tempo = gameSave.timeinsecs;
                _difficulty = gameSave.gameDifficulty;
                solution_string = gameSave.solution;
                Solution = sudoku_src.CreateMatriz(solution_string);
                DidYouDeactivateTimeInGame = gameSave.timeDeactivated;
            }
            configurations.timeInSeconds = S_tempo;

            Initiate();
            EnableButtons(false);
            sudoku_src.FadePanel(true, TelaJogo);
            sudoku_src.ResetSquare(AxA_squares);
            sudoku_src.ResetFont(AxA_squares);
            sudoku_src.EraseAllPencil(AxA_squares);

            SetGameOnSquares();
            StartCoroutine(SetFillOnSquares());
            SetDifText(_difficulty, lang);
            sudoku_src.FadePanel(false, MenuInicial);
            selectedSquare = null;
            EnableButtons(true);
        }
        isplaying = true;
    }

    // END OF SAVING FILES FUNCTIONS
    #endregion

    #region Set Dificult Text
    public void SetDifText(int difficulty, int lang)
    {
        if(lang == 1)
        {
            if (difficulty == 0)
                DificultyText.text = "Novice";
            if (difficulty == 1)
                DificultyText.text = "Easy";
            if (difficulty == 2)
                DificultyText.text = "Medium";
            if (difficulty == 3)
                DificultyText.text = "Hard";
            if (difficulty == 4)
                DificultyText.text = "Expert";
            if (difficulty == 5)
                DificultyText.text = "Evil";
        }
        
        if (lang == 2)
        {
            if (difficulty == 0)
                DificultyText.text = "Novato";
            if (difficulty == 1)
                DificultyText.text = "Fácil";
            if (difficulty == 2)
                DificultyText.text = "Médio";
            if (difficulty == 3)
                DificultyText.text = "Difícil";
            if (difficulty == 4)
                DificultyText.text = "Especialista";
            if (difficulty == 5)
                DificultyText.text = "Terror";
        }
    }
    #endregion

    #region Music and SFX Functions
    public void PlaySFX(int audioclip)
    {
        audioSourceSFX.clip = audio_holder.GetAudioClip(audioclip);
        audioSourceSFX.Play();
    }

    IEnumerator PlayMusic()
    {
        while (audioSourceMusic.isPlaying)
        {
            yield return new WaitForSeconds(2f);
        }

        if (canplaymusic)
        {
            audioSourceMusic.clip = audio_holder.GetMusic();
            audioSourceMusic.Play();
            musicCorroutine = PlayMusic();
            StartCoroutine(PlayMusic());
        }
    }

    public void DeactivateMusic(AudioSource audioSource)
    {
        canplaymusic = !canplaymusic;

        if (canplaymusic)
        {
            configurations.music = true;
            musicCorroutine = PlayMusic();
            StartCoroutine(PlayMusic());
        }
        else
        {
            configurations.music = false;
            musicCorroutine = PlayMusic();
            StopCoroutine(musicCorroutine);
            audioSource.Stop();
            audioSource.clip = null;
        }
        SaveConfig();
    }

    public void DeactivateSFX(AudioSource audioSource)
    {
        canplaySFX = !canplaySFX;

        if (canplaySFX)
        {
            audioSource.mute = false;
            configurations.sound = false;
        }
        else
        {
            audioSource.mute = true;
            configurations.sound = true;
            
        }
        SaveConfig();
    }
    #endregion

    #region Save and Load Config Functions
    public void SaveConfig()
    {
        int language = lang;
        bool sound = canplaySFX;
        bool music = canplaymusic;
        bool timeOn = configurations.TimerOn;
        string dateTime = DateTime.Today.ToString();
        int timesAdWasPlayedToday = adsManager.TimesAdWasPlayed;
        bool NoAdsPurchased = PlayerPurchasedNoAds;
        jsonSaving.ConfigSave(language, sound, music, timeOn, dateTime, timesAdWasPlayedToday, NoAdsPurchased);
    }

    public void SetConfig()
    {
<<<<<<< HEAD:PeacefulNumbers/Assets/Script/SudokuGame.cs
        if(jsonSaving.configSave != null)
        {
            lang = jsonSaving.configSave.language;
            canplaymusic = !jsonSaving.configSave.music;
            canplaySFX = !jsonSaving.configSave.sound;
            configurations.TimerOn = !jsonSaving.configSave.timeOn;
            PlayerPurchasedNoAds = jsonSaving.configSave.NoAdsPurchased;
            adsManager.ShouldCallAds = !jsonSaving.configSave.NoAdsPurchased;

            if (jsonSaving.configSave.NoAdsPurchased)
            {
                NoAdsButton.interactable = false;
            }

            DeactivateMusic(audioSourceMusic);
            musicToggle.OnOff = !jsonSaving.configSave.music;
            musicToggle.Toggle();
=======
        
        lang = jsonSaving.configSave.language;
        canplaymusic = !jsonSaving.configSave.music;
        canplaySFX = !jsonSaving.configSave.sound;
        configurations.TimerOn = !jsonSaving.configSave.timeOn;

        configurations.DeactivateTimer();
        TimerToggle.OnOff = !jsonSaving.gameSave.timeDeactivated;
        
        if(configurations.TimerOn == false)
            TimerToggle.Toggle();
        
>>>>>>> parent of 54cfe34e (Ver0.9):My project/Assets/Script/SudokuGame.cs

            DeactivateSFX(audioSourceSFX);
            SFXToggle.OnOff = !jsonSaving.configSave.sound;
            SFXToggle.Toggle();
        }

<<<<<<< HEAD:PeacefulNumbers/Assets/Script/SudokuGame.cs
        if(jsonSaving.gameSave != null)
        {
            configurations.DeactivateTimer();
            TimerToggle.OnOff = !jsonSaving.gameSave.timeDeactivated;

            if (configurations.TimerOn == false)
                TimerToggle.Toggle();
        }
        
=======
        DeactivateSFX(audioSourceSFX);
        SFXToggle.OnOff = !jsonSaving.configSave.sound;
        SFXToggle.Toggle();
>>>>>>> parent of 54cfe34e (Ver0.9):My project/Assets/Script/SudokuGame.cs
    }
    #endregion


}
