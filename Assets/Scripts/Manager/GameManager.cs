﻿using UnityEngine;

public class GameManager : MonoBehaviour {
    private int currentLevel = 0;
    public RectTransform fade;
    public RectTransform redFade;
    public float fadeTime = 1f;
    public LeanTweenType fadeEaseType = LeanTweenType.easeInOutQuart;
    public GameObject restartButton;

    void Start() {
        AddListener();
    }

    public void onRestartButtonClick() {
        fadeinRed();
        LeanTween.scale(restartButton, Vector3.one * 1.2f, 1).setEasePunch();
    }

    private void setCurrentLevel(object level) {
        currentLevel = (int)level;
    }

    private void onGameWin(object sender) {
        fadeinBlack();
        Debug.Log("Game win");
    }

    private void onGameLose(object sender) {
        fadeinRed();
        Debug.Log("Game lose");
    }

    private void fadeinBlack() {
        LeanTween.alpha(fade, 1f, fadeTime).setEase(fadeEaseType).setOnComplete(fadeoutBlack);
    }

    private void fadeoutBlack() {
        loadLevel();
        LeanTween.alpha(fade, 0f, fadeTime).setEase(fadeEaseType);
    }

    private void fadeinRed() {
        if(currentLevel == 0) {
            Debug.Log("Not in a level");
            return;
        }
        LeanTween.alpha(redFade, 1f, fadeTime).setEase(fadeEaseType).setOnComplete(fadeoutRed);
    }

    private void fadeoutRed() {
        restartLevel();
        LeanTween.alpha(redFade, 0f, fadeTime).setEase(fadeEaseType);
    }

    private void restartLevel() {
        EventManager.TriggerEvent(SystemEvents.LOAD_LEVEL, currentLevel);
    }

    private void loadLevel() {
        EventManager.TriggerEvent(SystemEvents.LOAD_LEVEL, currentLevel + 1);
    }

    private void AddListener() {
        EventManager.AddListener(SystemEvents.GAME_WIN, onGameWin);
        EventManager.AddListener(SystemEvents.GAME_LOSE, onGameLose);
        EventManager.AddListener(SystemEvents.SET_LEVEL, setCurrentLevel);
    }

    private void OnDestroy() {
        EventManager.RemoveListener(SystemEvents.GAME_WIN, onGameWin);
        EventManager.RemoveListener(SystemEvents.GAME_LOSE, onGameLose);
        EventManager.AddListener(SystemEvents.SET_LEVEL, setCurrentLevel);
    }
}
