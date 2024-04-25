﻿using Microsoft.AspNetCore.Mvc;

namespace MentallyStable.GitlabHelper.Extensions
{
    public static class HttpExtensions
    {
        public static string GetCurrentHost(this HttpContext context) => context.Request.Host.Value;
        public static string GetCurrentHost(this ControllerBase controller) => controller.HttpContext.GetCurrentHost();
    }
}
