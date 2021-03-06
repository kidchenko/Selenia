﻿using OpenQA.Selenium;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Selenia.Core
{
    public abstract class AbstractSeleniaElement : RealProxy
    {
        protected AbstractSeleniaElement() : base(typeof(SeleniaElement)) { }

        protected abstract IWebElement Delegate();

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;

            object result = null;
            if (methodCall.ArgCount == 0)
            {
                result = GetValue();
            }
            else
            {
                SetValue(methodCall.Args[0] as string);
            }

            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
        }

        private string GetValue()
        {
            return Delegate().GetAttribute("value");
        }

        private void SetValue(string text)
        {
            var element = Delegate();
            element.Clear();
            element.SendKeys(text);
        }
    }
}
