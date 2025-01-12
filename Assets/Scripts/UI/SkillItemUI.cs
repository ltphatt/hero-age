using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
    [SerializeField] SkillType skillType;
    [SerializeField] Image cooldownImage;
    private bool isStartCooldown = false;
    private float timer = 0f;
    PlayerSkill playerSkill;

    private void OnEnable()
    {
        PlayerSkill.OnSkillUsed += UpdateCooldown;
    }

    private void OnDisable()
    {
        PlayerSkill.OnSkillUsed -= UpdateCooldown;
    }

    private void Start()
    {
        isStartCooldown = false;
        playerSkill = FindObjectOfType<PlayerSkill>();
        cooldownImage.fillAmount = 0f;
        timer = GetSkillCooldown();
    }

    private void Update()
    {
        if (isStartCooldown)
        {
            timer -= Time.deltaTime;
            cooldownImage.fillAmount = timer / GetSkillCooldown();

            if (timer <= 0)
            {
                isStartCooldown = false;
                timer = GetSkillCooldown();
                cooldownImage.fillAmount = 0f;
            }
        }
    }

    private void UpdateCooldown(SkillType skillType)
    {
        if (skillType != this.skillType)
        {
            return;
        }

        isStartCooldown = true;
        cooldownImage.fillAmount = 1f;
        Debug.Log("Player is use skill " + skillType);
    }

    public float GetSkillCooldown()
    {
        float result = 0f;
        switch (skillType)
        {
            case SkillType.DASH:
                result = playerSkill.dashCooldown;
                break;
            case SkillType.AUTO_AIM:
                result = playerSkill.autoAimCooldown;
                break;
            case SkillType.TORNADO:
                result = playerSkill.tornadoCooldown;
                break;
            case SkillType.TIGER:
                result = playerSkill.tigerCooldown;
                break;
            case SkillType.DRAGON:
                result = playerSkill.dragonCooldown;
                break;
            default:
                break;

        }

        return result;
    }
}
