﻿using System;

namespace GameLogic
{
    /// <summary>
    /// Реализация игрока
    /// </summary>
    public class Player : IDestroyable, IAttacker, ICaster, IModified
    {
        public Player(int health, int healthMax, int power, int powerMax, int mana, int manaMax, 
            Deck<SpellCard> spells, Deck<SoliderCard> soliders)
        {
            this.spells = spells;
            this.soliders = soliders;

            Health = health;
            HealthMax = healthMax;

            Power = power;
            PowerMax = powerMax;

            Mana = mana;
            this.manaMax = manaMax;
        }

        private Deck<SoliderCard> soliders;
        /// <summary>
        /// Колода солдат игрока
        /// </summary>
        public Deck<SoliderCard> Soliders
        { get { return soliders; } }

        /// <summary>
        /// Вытаскивает карту солдата из колоды на стол
        /// </summary>
        /// <param name="solider"></param>
        public void CastSolider(SoliderCard solider/*,Position position*/)
        {
            //TODO

            //if (OnSoliderCasted != null) OnSoliderCasted.Invoke(this, new GameEventArgs(/*TODO*/));
        }

        /// <summary>
        /// Вызывается при вызове карты из колоды на стол
        /// </summary>
        public event InGameEvent OnSoliderCasted;

        #region IAttacker realization
        private int powerMax = -1;
        /// <summary>
        /// Максимальная сила атаки
        /// </summary>
        public int PowerMax
        {
            get { return powerMax; }
            protected set { if (value < 0) throw new ArgumentException(); }
        }

        private int power = -1;
        /// <summary>
        /// Сила атаки
        /// </summary>
        public int Power
        {
            get { return power; }
            protected set
            {
                if (value < 0) value = 0;
                if (value > powerMax) value = powerMax;

                var args = new GameEventArgs(power < value ? GameEventArgs.Means.Positive : GameEventArgs.Means.Negative, Context.power);

                power = value;

                if (OnPowerChanged != null)
                    OnPowerChanged.Invoke(this, args);
            }
        }

        /// <summary>
        /// Атака уничтожаемого объекта
        /// </summary>
        /// <param name="target">атакуемый объект</param>
        /// <returns></returns>
        public void Attack(IDestroyable target)
        {
            if (target == null) throw new ArgumentException("Invalid target value");

            //TODO
        }

        /// <summary>
        /// Изменение здоровья объекта
        /// </summary>
        /// <param name="health">Количество изменяемых единиц здоровья</param>
        public void DeltaPower(int delta)
        {
            Power += delta;
        }

        /// <summary>
        /// Событие, вызывающееся при изменении силы атаки
        /// </summary>
        public event InGameEvent OnPowerChanged;
        #endregion

        #region IDestroyable realization
        private int healthMax = -1;
        /// <summary>
        /// Максимальное здоровье карты
        /// </summary>
        public int HealthMax
        {
            get { return health; }
            protected set { if (value < 0) throw new ArgumentException("Wrong health value"); health = value; }
        }

        private int health = -1;
        /// <summary>
        /// Параметр здоровья карты
        /// </summary>
        public int Health
        {
            get { return health; }
            protected set
            {
                if (value <= 0)
                {
                    value = 0;

                    if (OnDeath != null)
                        OnDeath.Invoke(this, new GameEventArgs(GameEventArgs.Means.Death));

                    return;
                }
                if (value > healthMax) value = healthMax;

                var args = new GameEventArgs(health < value ? GameEventArgs.Means.Positive : GameEventArgs.Means.Negative, Context.health);

                health = value;

                if (OnHealthChanged != null)
                    OnHealthChanged.Invoke(this, args);
            }
        }

        /// <summary>
        /// Изменение здоровья объекта
        /// </summary>
        /// <param name="health">Количество изменяемых единиц здоровья</param>
        public void DeltaHealth(int delta)
        {
            if (health > 0) Health += delta;

            //TODO
        }

        /// <summary>
        /// Событие, вызывающееся при смерти
        /// </summary>
        public event InGameEvent OnDeath;

        /// <summary>
        /// Событие, вызывающееся при изменении количества здоровья
        /// </summary>
        public event InGameEvent OnHealthChanged;
        #endregion

        #region IModified realization
        /// <summary>
        /// Добавление нового модификатора
        /// </summary>
        /// <param name="modifier">Модификатор</param>
        public void TakeModifier(Modifier modifier)
        { if (modifier != null) modifier.Action(); }
        #endregion

        #region ICaseter realization
        readonly int manaMax = -1;
        /// <summary>
        /// Максимальное количество маны
        /// </summary>
        public int ManaMax
        { get { return manaMax; } }

        int mana = -1;
        /// <summary>
        /// Текущее количество маны
        /// </summary>
        public int Mana
        {
            get { return mana; }
            set
            {
                if (value < 0) value = 0;
                if (value > ManaMax) value = ManaMax;

                var args = new GameEventArgs(mana < value ? GameEventArgs.Means.Positive : GameEventArgs.Means.Negative, Context.mana);

                mana = value;

                if (OnManaChanged != null)
                    OnManaChanged.Invoke(this, args);
            }
        }

        public readonly Deck<SpellCard> spells;
        /// <summary>
        /// Колода заклинаний
        /// </summary>
        public Deck<SpellCard> Spells
        { get { return spells; } }

        /// <summary>
        /// Событие изменения количества маны
        /// </summary>
        public event InGameEvent OnManaChanged;

        /// <summary>
        /// Создание заклинания
        /// </summary>
        /// <param name="spell">Заклинание</param>
        /// /// <param name="target">Позиция на доске, к которой применяется заклинание</param>
        public void Cast(SpellCard spell/*, Position target*/)
        {
            //TODO
        }

        public void DeltaMana(int delta)
        { Mana += delta; }
        #endregion
    }
}