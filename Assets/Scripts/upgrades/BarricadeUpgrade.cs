using UnityEngine;

public class BarricadeUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private BarricadeDamageZone[] barricades;

    private void OnMouseDown()
    {
        if (!upgradeManager.upgradePending)
            return;

        Debug.Log("Barricade Upgrade Clicked");

        foreach (BarricadeDamageZone barricade in barricades)
        {
            barricade.UpgradeBarricadeStrength();

            Debug.Log(
                "New Damage Multiplier: " +
                barricade.GetDamageMultiplier()
            );
        }

        upgradeManager.ShowUpgradeMessage(
            "BARRICADES UPGRADED"
        );

        upgradeManager.UpgradeChosen();
    }
}