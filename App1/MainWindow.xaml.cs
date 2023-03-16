// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Monaco;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        BackdropHelper.TrySetBackdrop(this);
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        medit.SetLanguageAsync("csharp");
        medit.SetThemeAsync(EditorThemes.VisualStudioDark);
    }

    void SetMonacoEditorText(MonacoEditor meditor, string text)
    {
        meditor.MonacoEditorWebView.CoreWebView2.ExecuteScriptAsync($"window.editor.setValue('{text}')");
    }

    string GetMonacoEditorText(MonacoEditor meditor)
    {
        return meditor.MonacoEditorWebView.CoreWebView2.ExecuteScriptAsync($"window.editor.getValue()").AsTask().Result;
    }

    private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {
        SetMonacoEditorText(medit, "Hello & Welcome to SiCode IDE");
    }
}
