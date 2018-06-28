﻿using Hoangldp.Web.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hoangldp.Web.Core.Finder
{
    /// <summary>
    /// Provides information about types in the current web application.
    /// Optionally this class can look at all assemblies in the bin folder.
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        /// <summary>
        /// Gets or sets whether assemblies in the bin folder of the web application should be specifically checked for being loaded on application load. This is need in situations where plugins need to be loaded in the AppDomain after the application been reloaded.
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        public WebAppTypeFinder(CoreConfig coreConfig, ICoreFileProvider fileProvider = null) : base(fileProvider)
        {
            string pattern = "^Hoangldp";
            if (!string.IsNullOrEmpty(coreConfig.LibLoadingPattern))
            {
                pattern += "|" + coreConfig.LibLoadingPattern;
            }
            AssemblyRestrictToLoadingPattern = pattern;
        }

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            return AppContext.BaseDirectory;
        }

        /// <summary>
        /// Get assemblies
        /// </summary>
        /// <returns>Result</returns>
        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                var binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }

    }
}
