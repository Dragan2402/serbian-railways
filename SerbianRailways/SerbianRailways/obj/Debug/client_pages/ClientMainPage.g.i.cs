﻿#pragma checksum "..\..\..\client_pages\ClientMainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7D7A70C27BBE5813D2E29CD994021A79859F30144BEC65F35153C979EB4BBC2C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// ClientMainPage
    /// </summary>
    public partial class ClientMainPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\client_pages\ClientMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid client_grid;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\client_pages\ClientMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buyBookTicketsBtn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\client_pages\ClientMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showMyTicektsBtn;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\client_pages\ClientMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showLinesRidesBtn;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\client_pages\ClientMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showLinesGrid;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\client_pages\ClientMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logOutBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/SerbianRailways;component/client_pages/clientmainpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\client_pages\ClientMainPage.xaml"
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
            this.client_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.buyBookTicketsBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.showMyTicektsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\client_pages\ClientMainPage.xaml"
            this.showMyTicektsBtn.Click += new System.Windows.RoutedEventHandler(this.showMyTickets);
            
            #line default
            #line hidden
            return;
            case 4:
            this.showLinesRidesBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.showLinesGrid = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.logOutBtn = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\client_pages\ClientMainPage.xaml"
            this.logOutBtn.Click += new System.Windows.RoutedEventHandler(this.log_out);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

