﻿#pragma checksum "..\..\..\manager_pages\TrainsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CC76A0E0F48B3D03EBF05E2BABDA7CEC7C7EB1C80BA4FD492B3FC63CB3E29EA8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SerbianRailways.manager_pages;
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


namespace SerbianRailways.manager_pages {
    
    
    /// <summary>
    /// TrainsPage
    /// </summary>
    public partial class TrainsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\manager_pages\TrainsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTrains;
        
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
            System.Uri resourceLocater = new System.Uri("/SerbianRailways;component/manager_pages/trainspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\manager_pages\TrainsPage.xaml"
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
            this.dgTrains = ((System.Windows.Controls.DataGrid)(target));
            
            #line 21 "..\..\..\manager_pages\TrainsPage.xaml"
            this.dgTrains.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DGTrains_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\manager_pages\TrainsPage.xaml"
            this.dgTrains.MouseMove += new System.Windows.Input.MouseEventHandler(this.DGTrains_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 31 "..\..\..\manager_pages\TrainsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTrainBtn);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 32 "..\..\..\manager_pages\TrainsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTrainBtn);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\manager_pages\TrainsPage.xaml"
            ((System.Windows.Controls.Button)(target)).DragEnter += new System.Windows.DragEventHandler(this.DeleteBTN_DragEnter);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\manager_pages\TrainsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Drop += new System.Windows.DragEventHandler(this.DeleteBTN_Drop);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 34 "..\..\..\manager_pages\TrainsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReturnManagerPage);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

