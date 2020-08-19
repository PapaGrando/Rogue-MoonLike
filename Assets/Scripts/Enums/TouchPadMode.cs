/// <summary>
///     Режимы работы тачпада
///     <para>LeftRightButtons - Пространство разделено на 2 кнопки (движение вправо и влево)</para>
///     <para>MoveToTouchedPos - Персонаж передвигается в позицию указанную косанием</para>
/// </summary>
public enum TouchPadMode
{
    Disable,
    LeftRightButtons,
    MoveToTouchedPos
}