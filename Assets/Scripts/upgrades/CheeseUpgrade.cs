using UnityEngine;

public class CheeseUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private Health cheeseHealth;

    private void OnMouseDown()
    {
        if (!upgradeManager.upgradePending)
            return;

        cheeseHealth.RestoreFullHealth();

        upgradeManager.UpgradeChosen();

        upgradeManager.ShowUpgradeMessage("CHEESE HEALTH RESTORED");



    }
}