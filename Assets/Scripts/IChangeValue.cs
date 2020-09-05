namespace DeadLords.Interface
{
    /// <summary>
    /// Изменение числа/значения/кол-ва
    /// </summary>
    public interface IChangeValue
    {
        /// <summary>
        /// Непосредственное изменение числа/кол-ва на value в указанном направлении act. Не может превышать maxHP объекта
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="isHeal">True - heal, false - deal damage</param>
        void HealDam(int value, bool isHeal);

        /// <summary>
        /// Непосредственное изменение числа(ATTACK/HP) на value в указанном направлении act.
        /// </summary>
        /// <param name="att">Значение Attack</param>
        /// <param name="hp">Значение HP</param>
        /// <param name="act">Действие(+/+,-/-,+/-,-/+)</param>
        void BustDebust(int att, int hp, string act);
    }
}
