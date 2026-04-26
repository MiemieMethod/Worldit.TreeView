// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Worldit.TreeView
{
	public class InsertMarker : Control
	{
		static InsertMarker()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(InsertMarker),
				new FrameworkPropertyMetadata(typeof(InsertMarker)));
		}

		public static readonly DependencyProperty MarkerBrushProperty =
			DependencyProperty.Register("MarkerBrush", typeof(Brush), typeof(InsertMarker),
				new FrameworkPropertyMetadata(SystemColors.HighlightBrush));

		public Brush MarkerBrush {
			get { return (Brush)GetValue(MarkerBrushProperty); }
			set { SetValue(MarkerBrushProperty, value); }
		}
	}
}
