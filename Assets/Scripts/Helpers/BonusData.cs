public class BonusData
{
    /// <summary>
    /// Название бонуса/заклинания
    /// </summary>
    public string unikName;

    /// <summary>
    /// Полное описание бонуса
    /// </summary>
    public string fullName;

    /// <summary>
    /// Тип бонуса/заклинания(Для вызова в функции)
    /// </summary>
    public string type;

    /// <summary>
    /// Цель/цели заклинания/бонуса
    /// </summary>
    public string target;

    /// <summary>
    /// Значение атаки. Если оно не нужно - 0
    /// </summary>
    public int att;

    /// <summary>
    /// Значение HP. Если оно не нужно - 0
    /// </summary>
    public int hp;
}