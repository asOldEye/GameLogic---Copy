﻿using System;

namespace GameLogic
{
    /// <summary>
    /// Событийные аргументы внутри сессии
    /// </summary>
    public class GameEventArgs : EventArgs
    {
        /// <summary>
        /// контекст события
        /// </summary>
        public readonly Context? context;

        /// <summary>
        /// значение события
        /// </summary>
        public readonly Means means;
        public enum Means
        {
            Positive,
            Negative,
            Death,
            ModifierEnd
        }

        public GameEventArgs(Means means, Context? context = null )
        {
            this.context = context;
            this.means = means;
        }
    }
    /// <summary>
    /// Делегирует внутрисессийные события
    /// </summary>
    /// <param name="sender">объект-отправитель</param>
    /// <param name="e">сопровождающие аргументы</param>
    public delegate void InGameEvent(object sender, GameEventArgs e);
}
