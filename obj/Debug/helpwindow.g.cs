﻿#pragma checksum "..\..\helpwindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E40111765CF3E5CD193A373AE9A7738B025957D3C6F1D5302A65028D380ED84A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace vladnigger {
    
    
    /// <summary>
    /// helpwindow
    /// </summary>
    public partial class helpwindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal vladnigger.helpwindow HelpPop;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl MyTab12;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LessonHelp;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SupportButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Support;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CloseButton1;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\helpwindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close12;
        
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
            System.Uri resourceLocater = new System.Uri("/Zoomjoiner;component/helpwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\helpwindow.xaml"
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
            this.HelpPop = ((vladnigger.helpwindow)(target));
            return;
            case 2:
            this.MyTab12 = ((System.Windows.Controls.TabControl)(target));
            return;
            case 3:
            this.LessonHelp = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.SupportButton = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Support = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\helpwindow.xaml"
            this.Support.Click += new System.Windows.RoutedEventHandler(this.Support_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CloseButton1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.Close12 = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\helpwindow.xaml"
            this.Close12.Click += new System.Windows.RoutedEventHandler(this.Close12_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

