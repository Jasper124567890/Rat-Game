using UnityEngine;
using TMPro;
using System.Collections;

public class UpgradeManager : MonoBehaviour
{
    [Header("Upgrade Objects")]
    [SerializeField] private GameObject cheeseUpgrade;
    [SerializeField] private GameObject barricadeUpgrade;
    [SerializeField] private GameObject magazineUpgrade;

    [Header("UI")]
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private float messageDuration = 3f;

    public bool upgradePending;

    private Coroutine currentMessageRoutine;

    private void Start()
    {
        HideUpgradeObjects();

        if (upgradeText != null)
        {
            upgradeText.gameObject.SetActive(false);
        }
    }

    public void GiveUpgradeToken()
    {
        upgradePending = true;

        cheeseUpgrade.SetActive(true);
        barricadeUpgrade.SetActive(true);
        magazineUpgrade.SetActive(true);

        ShowUpgradeMessage("CHOOSE AN UPGRADE");

        Debug.Log("Choose an upgrade.");
    }

    public void HideUpgradeObjects()
    {
        cheeseUpgrade.SetActive(false);
        barricadeUpgrade.SetActive(false);
        magazineUpgrade.SetActive(false);
    }

    public void UpgradeChosen()
    {
        upgradePending = false;

        HideUpgradeObjects();
    }

    public void ShowUpgradeMessage(string message)
    {
        if (currentMessageRoutine != null)
        {
            StopCoroutine(currentMessageRoutine);
        }

        currentMessageRoutine =
            StartCoroutine(ShowMessageRoutine(message));
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        if (upgradeText == null)
            yield break;

        upgradeText.gameObject.SetActive(true);
        upgradeText.text = message;

        yield return new WaitForSeconds(messageDuration);

        upgradeText.gameObject.SetActive(false);

        currentMessageRoutine = null;
    }
}