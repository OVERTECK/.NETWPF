﻿using System;
using System.Collections.Generic;

namespace WpfApp.NET;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
