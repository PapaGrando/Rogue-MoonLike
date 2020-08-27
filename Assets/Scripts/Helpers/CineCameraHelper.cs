using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.U2D;

public class CineCameraHelper : MonoBehaviour
{
    [Header("GameObject отслеживаемой цели, если null, то будет найден первый PlayerController")]
    [SerializeField] private GameObject _target;
    [Header("Контейнер с коллайдером платформ, если null, будет найден первый с компонентом PlatformBounds")]
    [SerializeField] private GameObject _PlatformContainer;

    private CinemachineVirtualCamera _cineCamera;

    void Awake()
    {
        _cineCamera = GetComponent<CinemachineVirtualCamera>();

        _cineCamera.Follow = _target != null ? _target.transform : FindObjectOfType<PlayerController>().transform;

        //натройка коллизий камеры
        var cineConfiner = GetComponent<CinemachineConfiner>();
        if (cineConfiner == null)
        {
            cineConfiner = gameObject.AddComponent<CinemachineConfiner>();
            cineConfiner.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
        }

        cineConfiner.m_BoundingShape2D = _PlatformContainer.GetComponent<Collider2D>() ??
                                         FindObjectOfType<PlatformBounds>().gameObject.GetComponent<Collider2D>();
    }
}