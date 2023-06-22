﻿using Microsoft.AspNetCore.Mvc;

namespace Practical18_MVC.ViewModels;

public class ErrorViewModel
{
    public string? ErrorMessage { get; set; } = "Internal server error";
    public string? ErrorDetails { get; set; }
}
