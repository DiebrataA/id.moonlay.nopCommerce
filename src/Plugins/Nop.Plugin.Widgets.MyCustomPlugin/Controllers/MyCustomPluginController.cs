using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Media;
using Nop.Services.Configuration;
using Nop.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nop.Plugin.Widgets.MyCustomPlugin.Models;

namespace Nop.Plugin.Widgets.MyCustomPlugin.Controllers
{
    [Area(AreaNames.Admin)]
   public class MyCustomPluginController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public MyCustomPluginController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load setting fro a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var MyCustomPluginSettings = _settingService.LoadSetting<MyCustomPluginSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Message = MyCustomPluginSettings.Message,
                UseSandbox = MyCustomPluginSettings.UseSandbox
            };

            return View("~/Plugins/Widgets.MyCustomPlugin/Views/Configure.cshtml", model);
            
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var MyCustomPluginSettings = _settingService.LoadSetting<MyCustomPluginSettings>(storeScope);

            MyCustomPluginSettings.Message = model.Message;
            MyCustomPluginSettings.UseSandbox = model.UseSandbox;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSetting(MyCustomPluginSettings, x => x.Message, storeScope, false);
            _settingService.SaveSetting(MyCustomPluginSettings, x => x.UseSandbox, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
