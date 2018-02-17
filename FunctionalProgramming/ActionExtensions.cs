﻿using System;
using Unit = System.ValueTuple;

namespace FunctionalProgramming
{
    public static class ActionExtensions
    {
        public static Func<Unit> ToFunc(this Action action)
        {
            return () =>
            {
                action();
                return F.Unit;
            };
        }
    }
}