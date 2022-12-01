﻿using AppCitas.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AppCitas.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
} 