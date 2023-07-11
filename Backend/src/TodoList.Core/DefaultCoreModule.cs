using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TodoList.Core.Interfaces;
using TodoList.Core.Services;
using Module = Autofac.Module;

namespace TodoList.Core;
public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ToDoItemService>()
            .As<IToDoItemService>().InstancePerLifetimeScope();
    }
}


