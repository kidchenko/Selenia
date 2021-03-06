﻿using OpenQA.Selenium;

namespace Selenia.Core
{
    public interface SeleniaElement : IWebElement
    {
        string Value();

        SeleniaElement Value(string text);
    }
}