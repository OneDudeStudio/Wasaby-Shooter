using System;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    private bool _isMoving = false;

    private Vector3 _startPosition = new Vector2(-200, 0);
    private Vector3 _endPosition = Vector2.zero;
    private float _preshownBeats = 3;
    private TimeSpan _currentNoteTime;
    private RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;

    public void InitNote(TimeSpan noteTime)
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.position = new Vector3(-200, 0 ,0);
        _currentNoteTime = noteTime;
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
            MoveNote();
    }

    private void MoveNote()
    {
        _rectTransform.position = Vector2.Lerp(
                _startPosition,
                _endPosition,
                (_preshownBeats - (float)(_currentNoteTime.TotalSeconds - AudioSettings.dspTime)) / _preshownBeats);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("tr");
    }
}
