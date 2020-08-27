//Тут кастомные UnityEvent, которые возвращают значения
//todo : сделать на основе делегатов и удалить это

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventVector2 : UnityEvent<Vector2> { }
public class UnityEventBool : UnityEvent<bool> { }
/// <summary>
/// Событие, которое возвращает структуру словаря, где Key - бафф, Value - время действия
/// </summary>
public class UnityEventBuffsDictionary : UnityEvent<Dictionary<BuffStats, float>> {}

public class UnityEventMobStats : UnityEvent<MobStats> { }