using System;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.MyCustomPlugin.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.MyCustomPlugin.Components
{
    [ViewComponent(Name = "WidgetsMyCustomPlugin")]
    class WidgetsMyCustomPluginViewComponent : NopViewComponent
    {
        public static string ViewComponentName => "WidgetsMyCustomPlugin";
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;

        public WidgetsMyCustomPluginViewComponent(IStoreContext storeContext, ISettingService settingService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
        }
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var MyCustomPluginSettings = _settingService.LoadSetting<MyCustomPluginSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel
            {
                Message = MyCustomPluginSettings.Message,
                UseSandbox = MyCustomPluginSettings.UseSandbox
            };

            if (!model.UseSandbox && string.IsNullOrEmpty(model.Message))
                return Content("");

            return View("~/Plugins/Widgets.MyCustomPlugin/Views/PublicInfo.cshtml", model);
        }
    }
}
