﻿using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public class ControllerSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IControllerActionCreator, ControllerActionCreator>();
            container.RegisterSingleton<IControllerCreator, ControllerCreator>();
        }
    }
}
