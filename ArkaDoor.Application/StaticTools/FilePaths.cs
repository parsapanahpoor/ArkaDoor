﻿namespace ArkaDoor.Application.StaticTools;

public static class FilePaths
{

    #region Site

    public static string SiteFarsiName = "";
    public static string SiteAddress = "";
    public static string merchant = "";

    public static readonly string SiteLogo = "/content/images/site/logo/main/";
    public static readonly string SiteLogoServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/logo/main/");

    public static readonly string DefaultSiteLogo = "/content/images/site/logo/default/logo.png";
    public static readonly string SiteLogoThumb = "/content/images/site/logo/thumb/";
    public static readonly string SiteLogoThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/logo/thumb/");

    public static readonly string EmailBanner = "/content/images/site/emailBanner/main/";
    public static readonly string EmailBannerServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/emailBanner/main/");

    public static readonly string EmailBannerThumb = "/content/images/site/emailBanner/thumb/";
    public static readonly string EmailBannerThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/emailBanner/thumb/");

    #endregion

    #region UserAvatar

    public static readonly string UserAvatarPath = "/content/images/user/main/";
    public static readonly string UserAvatarPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/main/");

    public static readonly string UserAvatarPathThumb = "/content/images/user/thumb/";
    public static readonly string UserAvatarPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/thumb/");

    public static readonly string DefaultUserAvatar = "/content/images/user/DefaultAvatar.png";

    #endregion

}
