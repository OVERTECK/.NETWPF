using System;
using System.Collections.Generic;

namespace WpfApp.NET;

public partial class Product
{
    public uint Idproduct { get; set; }

    public string Title { get; set; } = null!;

    public int CategoriesId { get; set; }

    public string? TitlePath { get; set; }

    public virtual Category Categories { get; set; } = null!;
}
