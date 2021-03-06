﻿using System;
using System.IO;

namespace FunctionalProgramming.Chapter1.UnitIsBetterThenVoid
{
    public class TestConsole : IDisposable
    {
        private readonly TextWriter _originalOut;

        public TestConsole()
        {
            TextWriter = new StringWriter();
            _originalOut = Console.Out;
            Console.SetOut(TextWriter);
        }

        private readonly TextWriter TextWriter;
        
        public static implicit operator string(TestConsole testConsole)
        {
            return testConsole.TextWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOut);
        }
    }
}