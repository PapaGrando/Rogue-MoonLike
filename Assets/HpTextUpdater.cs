using TMPro;
using UnityEngine;

public class HpTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private MobStatsController _targetStatsController;

    void Awake()
    {
        _text = _text ?? GetComponent<TextMeshPro>();
        _targetStatsController = _targetStatsController ?? GetComponentInParent<MobStatsController>();

        _targetStatsController.MobStatsUpdated += UpdateHp;
    }

    private void UpdateHp(MobStats mobStats) => _text.text = mobStats.Health.ToString();
}
