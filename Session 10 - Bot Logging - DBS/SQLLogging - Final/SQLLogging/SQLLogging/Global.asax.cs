﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Azure;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SQLLogging
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        SqlConnection connection = null;

        protected void Application_Start()
        {
            // Setting up sql string connection
        
            string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BotDemo;Integrated Security=SSPI;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            Debug.WriteLine("Connection Success");

            Conversation.UpdateContainer(
              builder =>
              {
                  builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));
                  var store = new InMemoryDataStore();


                  builder.Register(c => store)
                            .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                            .AsSelf()
                            .SingleInstance();

                  // register our logger here
                  builder.RegisterType<SQLActivityLogger>().AsImplementedInterfaces().InstancePerDependency()
                    .WithParameter("conn", connection);

              });
         
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}