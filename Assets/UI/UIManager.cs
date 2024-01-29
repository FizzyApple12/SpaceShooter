using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI pressButtonText;

    public TextMeshProUGUI levelNumberText;
    public TextMeshProUGUI levelLabel;

    public RectTransform leftShutter;
    public RectTransform rightShutter;

    public Image levelNumberBackground;
    public Image welcomeBackground;

    public Renderer bgRenderer;
    Material bgMaterial;

    public const float shutterTarget = 725;

    Color welcomeColor = Color.white;
    Color welcomeBgColor = Color.black;

    void Start()
    {
        bgMaterial = bgRenderer.material;
    }

    void Update()
    {
        bgMaterial.SetFloat("_EnemyHitMult", GameManager.enemyHitMultiplier);

        float welcomeAlpha = Mathf.Clamp01(welcomeColor.a + (Time.deltaTime * (GameManager.gameStarted ? -1 : 1)));
        welcomeColor.a = welcomeAlpha;
        welcomeBgColor.a = welcomeAlpha;

        welcomeText.color = welcomeColor;
        pressButtonText.color = welcomeColor;
        welcomeBackground.color = welcomeBgColor;

        levelNumberText.text = "" + GameManager.level;

        if (GameManager.waitingForLevelTimeout)
        {
            float x = GameManager.betweenLevelTimer;

            Color levelNumberColor = Color.white;
            levelNumberColor.a = Mathf.Clamp01(-4.0f * Mathf.Abs(x - 2.5f) + 10.0f);

            Color backgroundColor = Color.black;
            backgroundColor.a = Mathf.Clamp01(-4.0f * Mathf.Abs(x - 2.5f) + 10.0f);

            levelNumberText.color = levelNumberColor;
            levelLabel.color = levelNumberColor;
            levelNumberBackground.color = backgroundColor;

            float transformPositioner = -Mathf.Pow((x - 2.5f) / 2.5f, 24) + 1;

            leftShutter.offsetMax = new Vector2(-1600.0f + transformPositioner * shutterTarget, leftShutter.offsetMax.y);
            rightShutter.offsetMin = new Vector2(1600.0f - transformPositioner * shutterTarget, leftShutter.offsetMin.y);
        }
        else
        {
            levelNumberText.color = Color.clear;
            levelLabel.color = Color.clear;
            levelNumberBackground.color = Color.clear;

            leftShutter.offsetMax = new Vector2(-1600.0f, leftShutter.offsetMax.y);
            rightShutter.offsetMin = new Vector2(1600.0f, leftShutter.offsetMin.y);
        }
    }
}
