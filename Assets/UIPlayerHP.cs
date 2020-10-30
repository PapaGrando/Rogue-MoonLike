using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHP : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private MobStatsController _mobStats;
    void Awake()
    {
        _slider = _slider ?? GetComponent<Slider>();
        _mobStats = FindObjectOfType<PlayerController>().GetComponent<MobStatsController>();

        _mobStats.MobStatsUpdated += UpdateHp;
    }

    void Start()
    {
        _slider.maxValue = _mobStats.GetDefaultValuesStats.Health;
        _slider.value = _mobStats.GetStats.Health;
    }

    private void UpdateHp(MobStats val) => _slider.value = val.Health;
}
