﻿#pragma checksum "..\..\..\client_pages\RailwayGridPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D8E61C745F39DFAA465B21ED1E75007F2C23E85CFDA766A938AE2ECFC0DC7A37"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Maps.MapControl.WPF;
using SerbianRailways.client_pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SerbianRailways.client_pages {
    
    
    /// <summary>
    /// RailwayGridPage
    /// </summary>
    public partial class RailwayGridPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\client_pages\RailwayGridPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgLines;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\client_pages\RailwayGridPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Maps.MapControl.WPF.Map LinesGridMap;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SerbianRailways;component/client_pages/railwaygridpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\client_pages\RailwayGridPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dgLines = ((System.Windows.Controls.DataGrid)(target));
            
            #line 21 "..\..\..\client_pages\RailwayGridPage.xaml"
            this.dgLines.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DGLines_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\client_pages\RailwayGridPage.xaml"
            this.dgLines.MouseMove += new System.Windows.Input.MouseEventHandler(this.DGLines_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LinesGridMap = ((Microsoft.Maps.MapControl.WPF.Map)(target));
            
            #line 29 "..\..\..\client_pages\RailwayGridPage.xaml"
            this.LinesGridMap.DragEnter += new System.Windows.DragEventHandler(this.MapDrop_DragEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\client_pages\RailwayGridPage.xaml"
            this.LinesGridMap.Drop += new System.Windows.DragEventHandler(this.MapDrop);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 30 "..\..\..\client_pages\RailwayGridPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearMap);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 31 "..\..\..\client_pages\RailwayGridPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReturnClientPage);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

