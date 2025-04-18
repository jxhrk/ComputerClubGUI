using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using CompClubGUI.Admin.API.Models;

namespace CompClubGUI.Admin;

public partial class DataFrameDataList : UserControl
{
    public virtual void SetData(List<object> data)
    {
        
    }
}

public class DataFrameDataList<T, T2> : DataFrameDataList where T2 : new()
{
    public DataFrameDataList()
    {
        InitializeComponent();

        var template = new FuncDataTemplate<T>((value, namescope) => new T2() as Control);
        DataList.ItemTemplate = template;
    }

    public override void SetData(List<object> data)
    {
        DataList.ItemsSource = data;
    }
}