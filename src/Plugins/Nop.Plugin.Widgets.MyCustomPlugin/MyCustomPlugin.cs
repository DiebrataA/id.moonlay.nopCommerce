using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using Nop.Services.Plugins;
using Nop.Services.Localization;
using System.IO.Enumeration;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Widgets.MyCustomPlugin.Components;

namespace Nop.Plugin.Widgets.MyCustomPlugin
{
    public class MyCustomPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        public MyCustomPlugin(ILocalizationService localizationService ,ISettingService settingService, IWebHelper webHelper)
        {
            _localizationService = localizationService;
           _webHelper = webHelper;
           _settingService = settingService;
        }


        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.HomepageTop};
        }
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsMyCustomPlugin/Configure";
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return WidgetsMyCustomPluginViewComponent.ViewComponentName;
        }

        /// <summary>
        /// Install Plugin
        /// </summary>
        public override void Install()
        {
            var settings = new MyCustomPluginSettings()
            {
                UseSandbox = true,
                Message = "Hello World"
            };
            _settingService.SaveSetting(settings);
            _localizationService.AddOrUpdatePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.UseSandbox", "UseSandbox");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.Message", "Message");
            base.Install();
        }

        public override void Uninstall()
        {
            _localizationService.DeletePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.UseSandbox");
            _localizationService.DeletePluginLocaleResource("Plugin.Widgets.MyCustomPlugin.Message");
            base.Uninstall();
        }
        /// <returns></returns>
       

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var menuItem = new SiteMapNode()
            {
                SystemName = "MyCustomPlugin",
                Title = "MyCustomPLugin Title",
                ControllerName = "MyCustomPlugin",
                ActionName = "Configure",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", "Admin"} },

            };
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);
        }
        public bool HideInWidgetList => true;
    }
}
