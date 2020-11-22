/// <summary>
/// Полное содержание карты. Для сохранения
/// </summary>
public class CardData
{
    /// <summary>
    /// Название карты. Для редактора
    /// </summary>
    public string cardName;
    /// <summary>
    /// Название карты внутри игры
    /// </summary>
    public string inGameName;
    /// <summary>
    /// Стоимость
    /// </summary>
    public int cost;
    /// <summary>
    /// Является ли эта карта заклинанием
    /// </summary>
    public bool manaSpell;
    /// <summary>
    /// Имя существа. Редакторское
    /// </summary>
    public string creatureName;
    /// <summary>
    /// Индекс бонуса
    /// </summary>
    public int cardsBonusIndex;
}