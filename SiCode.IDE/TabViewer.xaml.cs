// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SiCode.IDE
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabViewer : Window
    {
        public TabViewer()
        {
            this.InitializeComponent();

            Activated += TabViewWindowingSamplePage_Loaded;

            BackdropHelper b = new BackdropHelper();
            b.TrySetMicaSystemBackdrop(this, true);

            Tabs.TabItems.Add(GetNewTab());
        }

        TabViewItem GetNewTab()
        {
            TabViewItem t = new()
            {
                Header = "SiCode IDE",
                Content = new MainPage()
            };

            ((MainPage)t.Content).TargetTabItem = t;

            return t;
        }

        private void TabViewWindowingSamplePage_Loaded(object sender, WindowActivatedEventArgs e)
        {
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(CustomDragRegion);
            CustomDragRegion.MinWidth = 188;
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tabs_AddTabButtonClick(TabView sender, object args)
        {
            Tabs.TabItems.Add(GetNewTab());
        }
    }
}
