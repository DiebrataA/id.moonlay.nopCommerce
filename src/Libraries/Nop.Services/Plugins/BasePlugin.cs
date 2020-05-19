using System;
using System.ServiceModel.Channels;


namespace Nop.Services.Plugins
{
    /// <summary>
    /// Base plugin
    /// </summary>
    public abstract class BasePlugin : IPlugin
    {
        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public virtual string GetConfigurationPageUrl()
        {
            return null;
        }

        /// <summary>
        /// Gets or sets the plugin descriptor
        /// </summary>
        public virtual PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// Install plugin
        /// </summary>
        public virtual void Install() 
        {
            /*var settings = new MyCustomPluginSettings()
            {
                UseSandbox = true,
                Message = "Hello Moonlay"
            };
            _settingService.SaveSetting(settings);
            _localizationService.AddOrUpdatePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.UseSandbox", "UseSandbox");
                    _localizationService.AddOrUpdatePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.Message", "Message");
            base.Install();*/
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public virtual void Uninstall() 
        {
           /* _localizationService.DeletePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.UseSandbox");
            _localizationService.DeletePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.Message");
            base.Uninstall();*/
        }

        /// <summary>
        /// Prepare plugin to the uninstallation
        /// </summary>
        public virtual void PreparePluginToUninstall()
        {
            //any can put any custom validation logic here
            //throw an exception if this plugin cannot be uninstalled
            //for example, requires some other certain plugins to be uninstalled first
        }

      
    }
}
