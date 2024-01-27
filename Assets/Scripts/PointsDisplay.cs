using TMPro;
using UnityEngine;
using DG.Tweening;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _textPoints;

    private void OnEnable()
    {
        GameManager.ScoreIncreased += On_ScoreIncreased;
    }
    
    private void OnDisable()
    {
        GameManager.ScoreIncreased -= On_ScoreIncreased;
    }
    
    private void On_ScoreIncreased(int newPointsValue)
    {
        _textPoints.text = $"POINTS: {newPointsValue}";
        var sequence = DOTween.Sequence()
            .Append(_textPoints.DOFade(1, 0.1f))
            .Append(_textPoints.transform.DOScale(new Vector3(1 + 0.2f, 1 + 0.2f, 1 + 0.2f), .1f))
            .Append(_textPoints.transform.DOScale(1, .1f));
        sequence.Play();
    }
}
