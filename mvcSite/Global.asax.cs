﻿using Autofac;
using Autofac.Integration.Mvc;
using mvcSite.DAL;
using mvcSite.DAL.caching;
using mvcSite.DAL.DatabaseAccess;
using mvcSite.Persistence;
using mvcSite.Repositories;
using mvcSite.ViewModelBuilders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mvcSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CustomerDatabaseWriter>().As<ICustomerWriter>();
            builder.RegisterType<OrderDatabaseWriter>().As<IOrderWriter>();
            builder.RegisterType<OrderLineDatabaseWriter>().As<IOrderLineWriter>();
            builder.RegisterType<MovieDatabaseReader>().As<IAllMoviesOnlyReader>();
            builder.RegisterType<CachingMovieReader>().As<IMovieReader>();
            builder.RegisterType<MovieRepository>();
            builder.RegisterType<CustomerRepository>();
            builder.RegisterType<OrderRepository>();
            builder.RegisterType<OrderLineRepository>();
            builder.RegisterType<OrderCreationService>();
            builder.RegisterType<SessionManager>();
            builder.RegisterType<SessionWrapper>().As<ISessionWrapper>();
            builder.RegisterType<HomeViewModelBuilder>();
            builder.RegisterType<MovieViewModelBuilder>();
            builder.RegisterType<OrderViewModelBuilder>();
            builder.RegisterType<SummaryViewModelBuilder>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
        }
    }
}